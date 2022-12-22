using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{ 
    [SerializeField] float damageCoolTime;
    [SerializeField] float damageTime;
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHP>())
        {
            damageTime = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHP>())
        {
            damageTime += Time.deltaTime;
            if (damageTime >= damageCoolTime)
            {
                damageTime = 0;
                collision.GetComponent<PlayerHP>().Damage(damage);
            }

        }
    }
}
