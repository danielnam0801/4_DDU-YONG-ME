using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingEnemyAnim : EnemyBase
{
    Animator animator;

    FlyingEnemy enemy;

    public UnityEvent DeadInit;

    private void Start()
    {
        animator = GetComponent<Animator>();

        enemy = transform.parent.GetComponent<FlyingEnemy>();
    }

    public void IsChasing(bool boolean)
    {
        animator.SetBool("isChasing", boolean);
    }
    public void IsIdle(bool boolean)
    {
        animator.SetBool("IsIdle", boolean);
    }

    public void IsDead()
    {
        animator.SetTrigger("IsDead");
        DeadInit?.Invoke(); //죽었을때 초기화해줘야될것 인스펙터에서 넣어주면됨
    }

    public void Attack()// 어택 애니메이션 실행
    {
        enemy.isAttacking = true;
        animator.SetTrigger("IsAttack");
        //animator attack실행
    }

    public void FlyingAttackEnd() // 애니메이터 어택 애니메이션 뒤에 넣어줌
    {
        enemy.endAttack = true;
        enemy.isAttacking = false;
    }

}
