using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterMaker : MonoBehaviour
{
    [SerializeField] private GameObject holyWaterObj;
    [SerializeField] private float makeTime = 10f;
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>= makeTime)
        {
            Instantiate(holyWaterObj, transform);
            timer = 0;
        }
    }
}
