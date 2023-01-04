using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterMaker : MonoBehaviour
{
    [SerializeField] private GameObject holyWaterGlassObj;
    [SerializeField] private float makeTime = 10f;
    GameObject playerObj;
    float timer = 0;
    private void Start()
    {
        playerObj = transform.parent.gameObject;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>= makeTime)
        {
            Instantiate(holyWaterGlassObj,playerObj.transform.position, transform.rotation);
            timer = 0;
        }
    }
}
