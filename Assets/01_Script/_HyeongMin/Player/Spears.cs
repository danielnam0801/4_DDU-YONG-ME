using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spears : MonoBehaviour
{
    public bool godSpear = false;
    [SerializeField] private Weapon[] weapons;
    private void Start()
    {
        weapons = new Weapon[transform.childCount];
        SpearCount();
    }
    private void Update()
    {
        GodSpearCheck();
        SpearCount();
    }
    void GodSpearCheck()
    {
        if (godSpear)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].godSpear = true;
            }
        }
    }
    void SpearCount()
    {
        if(transform.childCount != weapons.Length)
        {
            weapons = new Weapon[transform.childCount];
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i] = transform.GetChild(i).GetComponent<Weapon>();
            }
        }
        //if(transform.childCount < weapons.Length)
        //{
        //    for (int i = transform.childCount ; i < weapons.Length; i++)
        //    {
        //        weapons[i + 1]= transform.GetChild(i + 1).GetComponent<Weapon>();
        //    }
        //}
    }
}
