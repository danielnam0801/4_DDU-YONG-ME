using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class PlayerEXP : MonoBehaviour
{
    PlayerUpgradeManager playerUpgradeManager;

    public int exp;
    public int maxExp;
    public int level;

    public UnityEvent LevelUpFeedBack;

    private void Start()
    {
        level = 1;
        maxExp = 6;
        playerUpgradeManager = GameObject.Find("InGameUI").GetComponent<PlayerUpgradeManager>();
    }
    void Update()
    {
        switch (level)
        {
            case 2:
                maxExp = 9;
                break;
            case 3:
                maxExp = 6;
                break;
            case 4:
                maxExp = 9;
                break;
            case 5:
                maxExp = 21;
                break;
        }

        LevelUp();
        if (level == 6)
        {
            playerUpgradeManager.transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = "LEVEL MAX";
        }
        else
        {
            playerUpgradeManager.transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = $"LEVEL {level}";
        }
    }

    public void ExpUp(int expUp)
    {
        LevelUpFeedBack?.Invoke();
        exp += expUp;
    }
    void LevelUp()
    {
        if (level != 6  )
        {
            if (exp >= maxExp)
            {
                level++;
                exp -= maxExp;
                playerUpgradeManager.ButtonActive();
            }
        }
    }
}
