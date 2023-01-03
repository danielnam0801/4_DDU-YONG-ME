using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPManager : EnemyBase
{
    [SerializeField]
    private int hp;
    public int HP { get { return hp; }
        set { this.hp = value; }
    }

    
    bool isDead = false;

    protected override void Awake()
    {
        this.hp = _enemy.HP();
    }

    private void Update()
    {
        if(this.hp <=0)
        {
            Debug.Log($"{_enemy.enemyName} : Die");
            if(isDead == false)
            {
                isDead = true;
                if (this.gameObject.CompareTag("FlyingEnemy"))
                {
                    FlyingEnemyAnim flyingAnim = transform.GetComponentInChildren<FlyingEnemyAnim>();
                    flyingAnim.IsDead();
                }
                else
                {
                    Debug.Log("존재하지 않는 Layer(적)");
                }
            }
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            //hp -= collision.gameObject.GetComponent<Weapon>().damage;
            hp -= 1;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            //hp -= collision.gameObject.GetComponent<Weapon>().damage;
            hp -= 1;
        }
    }
}