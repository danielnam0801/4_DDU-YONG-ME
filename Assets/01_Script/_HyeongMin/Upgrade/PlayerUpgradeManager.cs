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
        playerAbilityList.abilityArr = new string[10] { "�츣�޽��� �Ź�", "�ູ���� â", "���� ����", "������ ���� ����", "���ڰ�", "�߰� ����", "������ ���� �Ź�", "����", "������", "����" };
        playerAbilityList.abilityTextArr = new string[10] { "�̵��ӵ� 10% ����", "â ȸ�� ����, ���� �ִ� ���� 1�� ����", "���ݷ� 1 ����", "���� �ִ� ���� 1 ����", "�������� �޴� ���� 1ȸ ����", "�޴� ����� 0.5 ����, �̵��ӵ� 10% ����", "�� �ٱ� ����", "�ֺ� �� �̵��ӵ� 10% ����", "10�ʸ��� ���� ����, ���� ���� �� ���� ���", "�ֺ� �� �̵��ӵ� 5% ����, 3�ʵ��� ������ ��� " };
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
