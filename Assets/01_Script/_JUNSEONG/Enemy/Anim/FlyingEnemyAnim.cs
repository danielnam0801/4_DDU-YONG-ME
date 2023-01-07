using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingEnemyAnim : EnemyBase
{
    Animator animator;

    FlyingEnemy enemy;
    Shooting shoot;

    [SerializeField] UnityEvent DeadInit;
    [SerializeField] UnityEvent DeadInitInAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();

        enemy = transform.parent.GetComponent<FlyingEnemy>();
        shoot = transform.parent.GetComponent<Shooting>();
    }

    public void IsChasing(bool boolean)
    {
        animator.SetBool("isChasing", boolean);
    }
    public void IsIdle(bool boolean)
    {
        animator.SetBool("IsIdle", boolean);
    }

    public void DetectPlayer()
    {
        animator.SetTrigger("DetectPlayer");
    }

    public void IsDead()
    {
        animator.SetTrigger("IsDead");
        IsChasing(false);
        DeadInit?.Invoke(); //�׾����� �ʱ�ȭ����ߵɰ� �ν����Ϳ��� �־��ָ��
    }
   

    public void DeadInitInAnimation(float deadTime)
    {
        DeadInitInAnim?.Invoke();
        Destroy(transform.parent.gameObject, deadTime); //�� ��ü�� ���� // �� ��ũ��Ʈ�� ��ĭ �Ʒ��ֱ� ������ �θ� ������Ʈ�� ����� ��
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

    public void FlyingShoot()
    {
        shoot.Shoot();
    }

    public void FlyingAttack()
    {
        animator.SetTrigger("IsAttack");
    }

}
