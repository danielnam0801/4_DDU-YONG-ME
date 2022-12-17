using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHit : EnemyMovement
{
    public int hp;

    protected override void Awake()
    {
        //_enemyCollider = transform.parent.GetComponent<CapsuleCollider2D>();
        hp = _enemy.HP();    
    }
    private void Update()
    {
        if (Dir == playerPosDir)
        {
            print("현재 플레이어에게 공격받을수 없음!");
        }
        else
        {
            print("현재 플레이어에게 공격받을수 있음!");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {

            --hp;
            if (hp == 0)
            {
                _animator.SetTrigger("IsDead");
            }

            else if (hp > 0)
            {
                OnHit();
            }
        }

        
        //if (collision.gameObject.CompareTag("Weapon"))
        //{
        //    if (_enemy.enemyType == EnemyType.NormalEnemy)
        //    {
        //        //player의 데미지만큼 줄어듬

        //    }
        //    if (_enemy.enemyType == EnemyType.ShieldEnemy)
        //    {
        //        if (Dir == playerPosDir) //내가 보는 방향에 플레이어가 있을때
        //        {
        //            //damage를 입지 않는다
        //            //창을 현재 내 위치에 떨군다.

        //        }
        //        else
        //        {
        //            hp--;// Player의 데미지 만큼 줄어드는것으로 변경해야함
        //        }
        //    }
        //}

    }

    private void Dead()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        transform.parent.GetComponent<EnemyAttack>().enabled = false;
        transform.parent.GetComponent<EnemyMovement>().enabled = false;
        Destroy(transform.parent.GetComponent<Rigidbody2D>());
        GetComponent<EnemyOnHit>().enabled = false;
    }

    public void EndDeadAnim()// 애니메이션 이벤트로 연결
    {
        Destroy(gameObject, 0.5f);
    }

    void OnHit()
    {
        _animator.SetTrigger("IsHit");

    }

    public void HitOnWeapon()
    {
        //if (collision.gameObject.CompareTag("Weapon"))
        //{
            if (_enemy.enemyType == EnemyType.NormalEnemy)
            {
                //player의 데미지만큼 줄어듬

            }
            if (_enemy.enemyType == EnemyType.ShieldEnemy)
            {
                if (Dir == playerPosDir) //내가 보는 방향에 플레이어가 있을때
                {
                    //damage를 입지 않는다
                    //창을 현재 내 위치에 떨군다.

                }
                else
                {
                    hp--;// Player의 데미지 만큼 줄어드는것으로 변경해야함
                }
            }
        //}

    }
}
