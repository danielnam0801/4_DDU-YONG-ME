using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int stCross;
    public static PlayerHP instance;
    public bool isArmor;
    [SerializeField] private int hp;
    [SerializeField] private int maxHP;
    public int HP
    {
        get { return hp; }
        set { hp = value;}
    }
    public int MaxHP
    {
        
        get { return maxHP;}
        set { maxHP = value;}
    }
    public void Damage(int damage)
    {
        if (isArmor) damage -= 1; 
        HP -= damage;
    }
    public void Heal(int heal)
    {
        
        HP += heal;
    }
}
