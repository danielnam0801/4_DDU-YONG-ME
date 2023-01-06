using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerAbilityList playerAbilityList;
    [SerializeField] private GameObject upgradeButtons;
    [SerializeField] private Button[] button;
    [SerializeField] private int buttonUpDownRange;

    public float buttonTransformY;
    private void Start()
    {
        playerAbilityList.abilityArr = new string[10] { "헤르메스의 신발", "축복받은 창", "성물 장착", "쓸만한 가죽 가방", "십자가", "중갑 갑옷", "슬라임 점액 신발", "성경", "성수병", "성배" };
        playerAbilityList.abilityTextArr = new string[10] { "이동속도 10% 증가", "창 회수 가능, 무기 최대 개수 1개 제한", "공격력 1 증가", "무기 최대 개수 1 증가", "함정으로 받는 피해 1회 무시", "받는 대미지 0.5 감소, 이동속도 10% 감소", "벽 붙기 가능", "주변 적 이동속도 10% 감소", "10초마다 성수 생성, 성수 범위 내 적들 사망", "주변 적 이동속도 5% 감소, 3초동안 있으면 사망 " };
        buttonTransformY = Mathf.Abs(button[0].transform.position.y - transform.position.y) / buttonUpDownRange;
        upgradeButtons.SetActive(false);
    }
    public void ButtonActive()
    {
        upgradeButtons.SetActive(true);
        StartCoroutine(ButtonUP());
        Time.timeScale = 0;
    }

    IEnumerator ButtonUP()
    {
        AbilitySelect();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < buttonTransformY; j++)
            {
                button[i].transform.position += new Vector3(0, buttonUpDownRange, 0);
                yield return new WaitForSecondsRealtime(0.0001f);
            }
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }
    public IEnumerator ButtonDown()
    {
        for (int j = 0; j < buttonTransformY; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                button[i].transform.position -= new Vector3(0, buttonUpDownRange, 0);
            }
            yield return new WaitForSecondsRealtime(0.0001f);
        }
        upgradeButtons.SetActive(false);
    }

    void AbilitySelect()
    {

        playerAbilityList.abilityArrCount = new List<int>();
        List<int> arrayCount = new List<int>();

        for (int i = 0; i < playerAbilityList.abilityArr.Length; i++)
        {
            playerAbilityList.abilityArrCount.Add(i);
        }
        for (int i = 0; i < playerAbilityList.abilityArrCount.Count; ++i)
        {
            arrayCount.Add(i);
        }
        for (int i = 0; i < playerAbilityList.usedNumber.Count; i++)
        {
            if (arrayCount.Contains(playerAbilityList.usedNumber[i]))
            {
                arrayCount.Remove(playerAbilityList.usedNumber[i]);
            }

        }

        Debug.Log(arrayCount.Count);
        for (int i = 0; i < button.Length; ++i)
        {
            int random = Random.Range(0, arrayCount.Count);
            if (playerAbilityList.abilityLevel[arrayCount[random]] >= 2)
            {
                button[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerAbilityList.abilityArr[arrayCount[random]] + $" LV { playerAbilityList.abilityLevel[arrayCount[random]]} ";
            }
            else
            {
                button[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerAbilityList.abilityArr[arrayCount[random]];
            }
            button[i].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerAbilityList.abilityTextArr[arrayCount[random]];
            button[i].gameObject.transform.GetChild(2).GetComponent<Image>().sprite = playerAbilityList.spriteArr[arrayCount[random]];
            button[i].GetComponent<ButtonClickEvent>().buttonAbilityNumber = playerAbilityList.abilityNumber[arrayCount[random]];
            arrayCount.RemoveAt(random);
            Debug.Log(random);
        }
    }
}
