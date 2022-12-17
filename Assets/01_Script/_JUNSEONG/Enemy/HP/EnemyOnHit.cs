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
            print("���� �÷��̾�� ���ݹ����� ����!");
        }
        else
        {
            print("���� �÷��̾�� ���ݹ����� ����!");
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
        //        //player�� ��������ŭ �پ��

        //    }
        //    if (_enemy.enemyType == EnemyType.ShieldEnemy)
        //    {
        //        if (Dir == playerPosDir) //���� ���� ���⿡ �÷��̾ ������
        //        {
        //            //damage�� ���� �ʴ´�
        //            //â�� ���� �� ��ġ�� ������.

        //        }
        //        else
        //        {
        //            hp--;// Player�� ������ ��ŭ �پ��°����� �����ؾ���
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

    public void EndDeadAnim()// �ִϸ��̼� �̺�Ʈ�� ����
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
                //player�� ��������ŭ �پ��

            }
            if (_enemy.enemyType == EnemyType.ShieldEnemy)
            {
                if (Dir == playerPosDir) //���� ���� ���⿡ �÷��̾ ������
                {
                    //damage�� ���� �ʴ´�
                    //â�� ���� �� ��ġ�� ������.

                }
                else
                {
                    hp--;// Player�� ������ ��ŭ �پ��°����� �����ؾ���
                }
            }
        //}

    }
}
