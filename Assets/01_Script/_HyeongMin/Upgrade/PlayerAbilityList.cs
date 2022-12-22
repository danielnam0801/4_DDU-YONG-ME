using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityList : MonoBehaviour
{
    //능력 추가할 스크립트들
    [SerializeField] private Walk walk; //헤르메스, 중갑갑옷
    [SerializeField] private Attack attack; //축복창, 가죽가방
                                            //[SerializeField] private Enemy? ; //성물, 성경, 성수병, 성배
                                            //함정?
                                            //[SerializeField] private HP스크립트 hp; //중갑갑옷
    [SerializeField] private GameObject bibleObj;
    [SerializeField] private GameObject holyGrailObj;

    public string[] abilityArr;
    public string[] abilityTextArr;
    public int[] abilityNumber;
    public List<int> abilityArrCount = new List<int>();
    public List<int> usedNumber = new List<int>();

    private void Start()
    {
        abilityArr = new string[10]{"헤르메스의 신발","축복받은 창","성물 장착" ,"쓸만한 가죽 가방","십자가","중갑 갑옷","슬라임 점액 신발" ,"성경","성수병","성배" };
        abilityTextArr = new string[10] { "이동속도 10% 증가", "창 회수 가능, 무기 최대 개수 1개 제한", "공격력 1 증가", "무기 최대 개수 1 증가", "함정으로 받는 피해 1회 무시", "받는 대미지 0.5 감소, 이동속도 10%", "벽 붙기 가능", "주변 적 이동속도 10% 감소", "사용 시 범위 내 적들 사망", "주변 적 이동속도 5% 감소, 3초동안 있으면 사망 " };
    }
    public void AbilitySelect(int selectNumber)
    {
        Invoke("Ability" + selectNumber + "Upgrade", 0);
    }
    void Ability1Upgrade()
    {
        //walk.speed *= 1.1f;
        Debug.Log("헤르메스의 신발");
    }
    void Ability2Upgrade()
    {
        //attack.
        Debug.Log("축복받은 창");
    }
    void Ability3Upgrade()
    {
        //attack.
        Debug.Log("성물 장착");
    }
    void Ability4Upgrade()
    {
        //attack.
        Debug.Log("쓸만한 가죽 가방");
    }
    void Ability5Upgrade()
    {
        //함정 스크립트
        Debug.Log("십자가");
    }
    void Ability6Upgrade()
    {
        //hp.
        //walk.speed *= 0.9f;
        Debug.Log("중갑 갑옷");
    }
    void Ability7Upgrade()
    {
        //
        Debug.Log("슬라임 점액 신발");
    }
    void Ability8Upgrade()
    {
        //
        bibleObj.SetActive(true);
        Debug.Log("성경");
    }
    void Ability9Upgrade()
    {
        //
        Debug.Log("성수병");
    }
    void Ability10Upgrade()
    {
        //
        holyGrailObj.SetActive(true);
        Debug.Log("성배");
    }

}
