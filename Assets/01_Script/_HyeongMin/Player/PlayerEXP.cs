using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEXP : MonoBehaviour
{
    PlayerUpgradeManager playerUpgradeManager;

    public int exp;
    [SerializeField] private int maxExp;
    [SerializeField] private int maxExpUp;
    [SerializeField] private int level;
    private void Start()
    {
        playerUpgradeManager = GameObject.Find("InGameUI").GetComponent<PlayerUpgradeManager>();
    }
    void Update()
    {
        Test();
        LevelUp();
    }
    public void ExpUp(int expUp)
    {
        exp += expUp;
    }
    void LevelUp()
    {
        if(exp >= maxExp)
        {
            level++;
            exp -= maxExp;
            maxExp += maxExpUp;
            playerUpgradeManager.ButtonActive();
        }
    }
    void Test()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ExpUp(1);
        }
    }
}
