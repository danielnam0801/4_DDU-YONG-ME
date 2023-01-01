using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundEnemyAnim : EnemyBase
{

    protected AIBrain _brain;

    [SerializeField] UnityEvent DeadInit;
    [SerializeField] UnityEvent DeadInitInAnim;

    protected readonly int _attackHash = Animator.StringToHash("Attack");
    protected readonly int _DeadTriggerHash = Animator.StringToHash("Dead");
    protected readonly int _DeadBoolHash = Animator.StringToHash("IsDead");
    protected readonly int _walkHash = Animator.StringToHash("Walk");

    protected override void Awake()
    {
       _brain = transform.parent.GetComponent<AIBrain>();
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

    public void SetEndOfAttackAnimation()
    {
        _brain.AIActionData.isAttack = false;
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attackHash);
    }

    
    public void EnemyDead()
    {
        _animator.SetTrigger("IsDead");
    }


}
