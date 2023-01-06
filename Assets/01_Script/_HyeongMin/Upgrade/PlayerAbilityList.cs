using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityList : MonoBehaviour
{
    //능력 추가할 스크립트들
    [SerializeField] private Walk walk; //헤르메스, 중갑갑옷
    [SerializeField] private Attack attack; //축복창, 가죽가방
    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerHP playerHp; //중갑갑옷
    [SerializeField] private GameObject bibleObj;
    [SerializeField] private GameObject holyWater;
    [SerializeField] private GameObject holyGrailObj;

    GameObject playerObj;

    public string[] abilityArr;
    public string[] abilityTextArr;
    public int itemNumber = 0;
    public int[] abilityNumber;
    public List<int> abilityArrCount = new List<int>();
    public List<int> usedNumber = new List<int>();
    public List<int> levelUpNumber = new List<int>();
    public List<int> finalList = new List<int>();
    public List<Sprite> spriteArr = new List<Sprite>();

    public int[] abilityLevel;
    
    private void Start()
    {
        abilityLevel = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        playerObj = GameObject.Find("Player");
        walk = playerObj.GetComponent<Walk>();
        attack = playerObj.GetComponent<Attack>();
        playerHp = playerObj.GetComponent<PlayerHP>();
        bibleObj = playerObj.transform.GetChild(1).gameObject;
        holyWater = playerObj.transform.GetChild(2).gameObject;
        holyGrailObj = playerObj.transform.GetChild(3).gameObject;
    }
    public void AbilitySelect(int selectNumber)
    {
        Invoke("Ability" + selectNumber + "Upgrade", 0);
    }
    void Ability1Upgrade()
    {
        walk.speed *= 1.1f;
        Debug.Log("헤르메스의 신발");
    }
    void Ability2Upgrade()
    {
        attack.GodON(FindObjectOfType<Weapon>());
        Debug.Log("축복받은 창");
        usedNumber.Add(4);
    }
    void Ability3Upgrade()
    {
        attack.damage += 1;
        Debug.Log("성물 장착");
    }
    void Ability4Upgrade()
    {
        attack.Spear_Count += 1;
        Debug.Log("쓸만한 가죽 가방");
    }
    void Ability5Upgrade()
    {
        //함정 스크립트
        Debug.Log("십자가");
    }
    void Ability6Upgrade()
    {
        playerHp.isArmor = true;
        walk.speed *= 0.9f;
        Debug.Log("중갑 갑옷");
    }
    void Ability7Upgrade()
    {
        walk.slimeShoes = true;
        Debug.Log("슬라임 점액 신발");
    }
    void Ability8Upgrade()
    {
        bibleObj.SetActive(true);
        Debug.Log("성경");
    }
    void Ability9Upgrade()
    {
        holyWater.SetActive(true);
        Debug.Log("성수병");
    }
    void Ability10Upgrade()
    {
        holyGrailObj.SetActive(true);
        Debug.Log("성배");
    }

}
