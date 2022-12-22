using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityList : MonoBehaviour
{
    //�ɷ� �߰��� ��ũ��Ʈ��
    [SerializeField] private Walk walk; //�츣�޽�, �߰�����
    [SerializeField] private Attack attack; //�ູâ, ���װ���
                                            //[SerializeField] private Enemy? ; //����, ����, ������, ����
                                            //����?
                                            //[SerializeField] private HP��ũ��Ʈ hp; //�߰�����
    [SerializeField] private GameObject bibleObj;
    [SerializeField] private GameObject holyGrailObj;

    public string[] abilityArr;
    public string[] abilityTextArr;
    public int[] abilityNumber;
    public List<int> abilityArrCount = new List<int>();
    public List<int> usedNumber = new List<int>();

    private void Start()
    {
        abilityArr = new string[10]{"�츣�޽��� �Ź�","�ູ���� â","���� ����" ,"������ ���� ����","���ڰ�","�߰� ����","������ ���� �Ź�" ,"����","������","����" };
        abilityTextArr = new string[10] { "�̵��ӵ� 10% ����", "â ȸ�� ����, ���� �ִ� ���� 1�� ����", "���ݷ� 1 ����", "���� �ִ� ���� 1 ����", "�������� �޴� ���� 1ȸ ����", "�޴� ����� 0.5 ����, �̵��ӵ� 10%", "�� �ٱ� ����", "�ֺ� �� �̵��ӵ� 10% ����", "��� �� ���� �� ���� ���", "�ֺ� �� �̵��ӵ� 5% ����, 3�ʵ��� ������ ��� " };
    }
    public void AbilitySelect(int selectNumber)
    {
        Invoke("Ability" + selectNumber + "Upgrade", 0);
    }
    void Ability1Upgrade()
    {
        //walk.speed *= 1.1f;
        Debug.Log("�츣�޽��� �Ź�");
    }
    void Ability2Upgrade()
    {
        //attack.
        Debug.Log("�ູ���� â");
    }
    void Ability3Upgrade()
    {
        //attack.
        Debug.Log("���� ����");
    }
    void Ability4Upgrade()
    {
        //attack.
        Debug.Log("������ ���� ����");
    }
    void Ability5Upgrade()
    {
        //���� ��ũ��Ʈ
        Debug.Log("���ڰ�");
    }
    void Ability6Upgrade()
    {
        //hp.
        //walk.speed *= 0.9f;
        Debug.Log("�߰� ����");
    }
    void Ability7Upgrade()
    {
        //
        Debug.Log("������ ���� �Ź�");
    }
    void Ability8Upgrade()
    {
        //
        bibleObj.SetActive(true);
        Debug.Log("����");
    }
    void Ability9Upgrade()
    {
        //
        Debug.Log("������");
    }
    void Ability10Upgrade()
    {
        //
        holyGrailObj.SetActive(true);
        Debug.Log("����");
    }

}
