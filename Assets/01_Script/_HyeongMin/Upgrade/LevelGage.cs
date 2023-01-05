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
        playerEXP = GameObject.Find("Player1").GetComponent<PlayerEXP>();
    }
    void Update()
    {
        slider.maxValue = playerEXP.maxExp;
        slider.value = playerEXP.exp;
        slider.minValue = 0;
    }
}
