using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int stCross;
    public static PlayerHP instance;
    public bool isArmor;
    public int hp;
    public int MaxHp;
    private void Awake()
    {
        hp = MaxHp;
    }
    public void Damage(int damage)
    {
        Debug.Log(damage);
        if (isArmor) damage -= 1;
        hp -= damage;
    }
    public void Heal(int heal)
    {
        
        hp += heal;
    }
}
