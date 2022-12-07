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

    public void Attack()// 어택 애니메이션 실행
    {
        enemy._isCanAttack = false;
        enemy._isAttacking = true;
        animator.SetTrigger("IsAttack");
        //animator attack실행
    }

    public void FlyingAttackEnd() // 애니메이터 어택 애니메이션 뒤에 넣어줌
    {
        enemy.endAttack = true;
        enemy._isAttacking = false;
    }
}
