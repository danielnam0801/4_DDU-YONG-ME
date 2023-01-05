using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickEvent : MonoBehaviour
{
    [SerializeField] private PlayerAbilityList playerAbilityList;
    [SerializeField] private PlayerUpgradeManager playerUpgradeManager;
    [SerializeField] private AudioSource audio;
    public int buttonAbilityNumber;
    public void ButtonClick()
    {
        audio.Play();
        playerAbilityList.AbilitySelect(buttonAbilityNumber);
        playerAbilityList.usedNumber.Add(buttonAbilityNumber - 1);
        StartCoroutine(playerUpgradeManager.ButtonDown());
        Time.timeScale = 1;
    }
}
