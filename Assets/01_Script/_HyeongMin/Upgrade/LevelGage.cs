using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelGage : MonoBehaviour
{
    PlayerEXP playerEXP;
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
        playerEXP = GameObject.Find("Player").GetComponent<PlayerEXP>();
    }
    void Update()
    {
        if (playerEXP.level != 6)
        {
            slider.maxValue = playerEXP.maxExp;
            slider.value = playerEXP.exp;
            slider.minValue = 0;
        }
        else
        {
            slider.maxValue = 1;
            slider.value = 1;
        }
    }
}
