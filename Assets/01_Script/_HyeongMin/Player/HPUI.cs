using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HPUI : MonoBehaviour
{
    PlayerHP playerHP;
    private void Awake()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerHP>();
    }
    private void Update()
    {
        switch (playerHP.hp)
        {
            case 6:
                for (int i = 0; i < 3; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().fillAmount = 1;
                }
                break;
            case 5:
                for (int i = 0; i < 2; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().fillAmount = 1;
                }
                transform.GetChild(2).GetComponent<Image>().fillAmount = 0.5f;
                break;
            case 4:
                for (int i = 0; i < 2; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().fillAmount = 1;
                }
                transform.GetChild(2).GetComponent<Image>().fillAmount = 0f;
                break;
            case 3:

                transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                transform.GetChild(1).GetComponent<Image>().fillAmount = 0.5f;
                transform.GetChild(2).GetComponent<Image>().fillAmount = 0f;
                break;
            case 2:
                transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                for (int i = 1; i < 3; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().fillAmount = 0;
                }
                break;
            case 1:
                transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
                for (int i = 1; i < 3; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().fillAmount = 0;
                }
                break;
            default:
                for (int i = 0; i < 3; i++)
                {
                    transform.GetChild(i).GetComponent<Image>().fillAmount = 0;
                }
                //load
                break;
        }
    }
}