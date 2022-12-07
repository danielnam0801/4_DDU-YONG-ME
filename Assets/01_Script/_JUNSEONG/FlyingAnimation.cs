using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAnimation : MonoBehaviour
{
    Animator animator;
    FlyingEnemy enemy;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = transform.parent.GetComponent<FlyingEnemy>();
    }

    public void IsIdle(bool boolean)
    {
        animator.SetBool("IsIdle", boolean);
    }

    public void IsDead()
    {
        animator.SetTrigger("IsDead");
    }

    public void Attack()// ���� �ִϸ��̼� ����
    {
        enemy._isCanAttack = false;
        enemy._isAttacking = true;
        animator.SetTrigger("IsAttack");
        //animator attack����
    }

    public void FlyingAttackEnd() // �ִϸ����� ���� �ִϸ��̼� �ڿ� �־���
    {
        enemy.endAttack = true;
        enemy._isAttacking = false;
    }
}
