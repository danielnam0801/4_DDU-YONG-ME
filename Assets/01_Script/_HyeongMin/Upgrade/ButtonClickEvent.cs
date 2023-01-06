using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{
    [SerializeField] private PlayerAbilityList playerAbilityList;
    [SerializeField] private PlayerUpgradeManager playerUpgradeManager;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private GameObject inGameUI;
    public int buttonAbilityNumber;
    private void Awake()
    {
        clickSound = inGameUI.transform.GetChild(1).GetComponent<AudioSource>();
    }
    public void ButtonClick()
    {
        clickSound.Play();
        playerAbilityList.AbilitySelect(buttonAbilityNumber);
        if (!playerAbilityList.usedNumber.Contains(buttonAbilityNumber - 1) && usedCheck(buttonAbilityNumber - 1))
        {
            inGameUI.transform.GetChild(3).GetChild(playerAbilityList.itemNumber).GetComponent<Image>().sprite = transform.GetChild(2).GetComponent<Image>().sprite;
            playerAbilityList.usedNumber.Add(buttonAbilityNumber - 1);
            playerAbilityList.itemNumber++;
        }
        else
        {
            if (playerAbilityList.abilityLevel[buttonAbilityNumber - 1] == 1)
            {
                inGameUI.transform.GetChild(3).GetChild(playerAbilityList.itemNumber).GetComponent<Image>().sprite = transform.GetChild(2).GetComponent<Image>().sprite;
                playerAbilityList.itemNumber++;
                playerAbilityList.levelUpNumber.Add(buttonAbilityNumber - 1);
            }
            if (playerAbilityList.levelUpNumber.Contains(buttonAbilityNumber - 1))
            {
                playerAbilityList.abilityLevel[buttonAbilityNumber - 1]++;
            }
        }


        StartCoroutine(playerUpgradeManager.ButtonDown());
        Time.timeScale = 1;
    }
    bool usedCheck(int i)
    {
        if (i != 0 && i != 2 && i != 3 && i != 4)
        {
            return true;
        }
        return false;
    }
}

