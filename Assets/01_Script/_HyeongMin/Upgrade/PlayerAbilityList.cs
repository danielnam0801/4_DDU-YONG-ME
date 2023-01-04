using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private Spears spears;

    GameObject playerObj;

    public string[] abilityArr;
    public string[] abilityTextArr;
    public int[] abilityNumber;
    public List<int> abilityArrCount = new List<int>();
    public List<int> usedNumber = new List<int>();

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        walk = playerObj.GetComponent<Walk>();
        attack = playerObj.GetComponent<Attack>();
        playerHp = playerObj.GetComponent<PlayerHP>();
        bibleObj = playerObj.transform.GetChild(1).gameObject;
        holyWater = playerObj.transform.GetChild(2).gameObject;
        holyGrailObj = playerObj.transform.GetChild(3).gameObject;

        spears = GameObject.Find("Spears").GetComponent<Spears>();

        abilityArr = new string[10] { "�츣�޽��� �Ź�", "�ູ���� â", "���� ����", "������ ���� ����", "���ڰ�", "�߰� ����", "������ ���� �Ź�", "����", "������", "����" };
        abilityTextArr = new string[10] { "�̵��ӵ� 10% ����", "â ȸ�� ����, ���� �ִ� ���� 1�� ����", "���ݷ� 1 ����", "���� �ִ� ���� 1 ����", "�������� �޴� ���� 1ȸ ����", "�޴� ����� 0.5 ����, �̵��ӵ� 10% ����", "�� �ٱ� ����", "�ֺ� �� �̵��ӵ� 10% ����", "10�ʸ��� ���� ����, ���� ���� �� ���� ���", "�ֺ� �� �̵��ӵ� 5% ����, 3�ʵ��� ������ ��� " };
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
        spears.godSpear = true;
        attack.Spear_Count = 1;
        Debug.Log("�ູ���� â");
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
