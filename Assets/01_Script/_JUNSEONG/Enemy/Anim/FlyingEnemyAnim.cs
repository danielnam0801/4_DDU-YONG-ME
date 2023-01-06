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
        DeadInit?.Invoke(); //죽었을때 초기화해줘야될것 인스펙터에서 넣어주면됨
    }
   

    public void DeadInitInAnimation(float deadTime)
    {
        DeadInitInAnim?.Invoke();
        Destroy(transform.parent.gameObject, deadTime); //적 자체를 지움 // 이 스크립트는 한칸 아래있기 때문에 부모 오브젝트를 지우면 됨
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

    public void FlyingShoot()
    {
        shoot.Shoot();
    }

    public void FlyingAttack()
    {
        animator.SetTrigger("IsAttack");
    }

}
