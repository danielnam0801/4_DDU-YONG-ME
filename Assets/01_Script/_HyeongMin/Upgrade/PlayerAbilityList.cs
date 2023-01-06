using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityList : MonoBehaviour
{
    //�ɷ� �߰��� ��ũ��Ʈ��
    [SerializeField] private Walk walk; //�츣�޽�, �߰�����
    [SerializeField] private Attack attack; //�ູâ, ���װ���
    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerHP playerHp; //�߰�����
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
        Debug.Log("�츣�޽��� �Ź�");
    }
    void Ability2Upgrade()
    {
        attack.GodON(FindObjectOfType<Weapon>());
        Debug.Log("�ູ���� â");
        usedNumber.Add(4);
    }
    void Ability3Upgrade()
    {
        attack.damage += 1;
        Debug.Log("���� ����");
    }
    void Ability4Upgrade()
    {
        attack.Spear_Count += 1;
        Debug.Log("������ ���� ����");
    }
    void Ability5Upgrade()
    {
        //���� ��ũ��Ʈ
        Debug.Log("���ڰ�");
    }
    void Ability6Upgrade()
    {
        playerHp.isArmor = true;
        walk.speed *= 0.9f;
        Debug.Log("�߰� ����");
    }
    void Ability7Upgrade()
    {
        walk.slimeShoes = true;
        Debug.Log("������ ���� �Ź�");
    }
    void Ability8Upgrade()
    {
        bibleObj.SetActive(true);
        Debug.Log("����");
    }
    void Ability9Upgrade()
    {
        holyWater.SetActive(true);
        Debug.Log("������");
    }
    void Ability10Upgrade()
    {
        holyGrailObj.SetActive(true);
        Debug.Log("����");
    }

}
