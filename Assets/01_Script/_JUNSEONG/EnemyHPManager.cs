using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPManager : EnemyBase
{
    int hp;

    bool isDead = false;

    protected override void Awake()
    {
        this.hp = _enemy.HP();
    }

    private void Update()
    {
        if(hp <=0)
        {
            if(isDead == false)
            {
                isDead = true;
                if (this.gameObject.CompareTag("FlyingEnemy"))
                {
                    FlyingEnemyAnim flyingAnim = transform.parent.GetComponent<FlyingEnemyAnim>();
                    flyingAnim.IsDead();
                }
                else if (this.gameObject.CompareTag("GroundEnemy"))
                {
                    GroundEnemyAnim groundAnim = transform.parent.GetComponent<GroundEnemyAnim>();
                    groundAnim.EnemyDead();
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
}