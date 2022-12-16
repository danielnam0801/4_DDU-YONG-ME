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
        DeadInit?.Invoke(); //�׾����� �ʱ�ȭ����ߵɰ� �ν����Ϳ��� �־��ָ��
    }

    public void Attack()// ���� �ִϸ��̼� ����
    {
        enemy.isAttacking = true;
        animator.SetTrigger("IsAttack");
        //animator attack����
    }

    public void FlyingAttackEnd() // �ִϸ����� ���� �ִϸ��̼� �ڿ� �־���
    {
        enemy.endAttack = true;
        enemy.isAttacking = false;
    }

}
