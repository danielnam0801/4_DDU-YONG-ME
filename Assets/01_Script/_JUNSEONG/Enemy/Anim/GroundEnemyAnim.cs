using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundEnemyAnim : EnemyBase
{
    EnemyMovement enemyMove;
    EnemyAttack enemyAttack;

    [SerializeField] UnityEvent DeadInit;
    [SerializeField] UnityEvent DeadInitInAnim;

    protected override void Awake()
    {
       enemyMove = transform.parent.GetComponent<EnemyMovement>();
       enemyAttack = transform.parent.GetComponent<EnemyAttack>();
    }

    public void EnemeyAttack()
    {
        if(_enemy.enemyType != EnemyType.ShieldEnemy)
        {
             int randAttack = UnityEngine.Random.Range(1, 5); //현재 기본 : 스페셜 = 3 : 1 비율
             _animator.SetInteger("Attack", randAttack);
        }
        _animator.SetTrigger("canAttack");
        _animator.SetBool("IsAttacking", true);
    }

    public void EndAttacking()
    {
        Debug.Log("공격이 끝났습니다");
        enemyAttack.endAttacking = true;
        enemyAttack._isAttacking = false;
        _animator.SetBool("IsAttacking", false);
    }

    public void EnemyMoving(bool boolean)
    {
        _animator.SetBool("moving", boolean);
    }

    public void EnemyDead()
    {
        _animator.SetTrigger("IsDead");
    }
}
