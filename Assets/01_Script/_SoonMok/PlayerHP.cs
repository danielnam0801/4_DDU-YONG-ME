using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public int stCross;
    public static PlayerHP instance;
    public bool isArmor;
    public bool isDead;
    public int hp;
    public int MaxHp;

    public UnityEvent OnDie;
    public UnityEvent OnHit;

    private void Awake()
    {
        hp = MaxHp;
    }
    public void Damage(int damage)
    {
        if (isDead == true) return;
        Debug.Log(damage);
        if (isArmor) damage -= 1;
        hp -= damage;
        OnHit?.Invoke();
        if (hp <= 0) {
            isDead = true;
            Die();
        }
        
    }
    public void Heal(int heal)
    {
        hp += heal;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
