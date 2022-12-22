using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float speedOpen = 3f;
    [SerializeField] private float weightOpen = 1f;
    float weightNormal;
    public bool doorOpen = false;
    public bool doorClose = false;
    private void Start()
    {
        weightNormal = weightOpen;
    }
    void Update()
    {
        WeightOpenCheck();
        DoorOpen();
        DoorClose();
    }
    void WeightOpenCheck()
    {
        if(weightOpen <= 0)
        {
            weightOpen = 0;
            transform.position = new Vector3(transform.position.x, (int)transform.position.y, 0);
            doorOpen = false;
        }
        if (weightOpen > weightNormal)
        {
            weightOpen = weightNormal;
            transform.position = new Vector3(transform.position.x, (int)transform.position.y, 0);
            doorClose = false;
        }
    }
    void DoorOpen()
    {
        if(doorOpen == true)
        {
            transform.position += new Vector3(0, speedOpen * Time.deltaTime, 0);
            weightOpen -= Time.deltaTime;
        }
    }
    void DoorClose()
    {
        if (doorClose == true)
        {
            transform.position -= new Vector3(0, speedOpen * Time.deltaTime, 0);
            weightOpen += Time.deltaTime;
        }
    }
}
