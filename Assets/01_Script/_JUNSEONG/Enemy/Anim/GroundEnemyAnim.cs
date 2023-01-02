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
    protected readonly int _DeadTriggerHash = Animator.StringToHash("Death");
    protected readonly int _DeadBoolHash = Animator.StringToHash("IsDead");
    protected readonly int _walkHash = Animator.StringToHash("Walk");
    protected readonly int _IdleHash = Animator.StringToHash("Idle");


    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
       _brain = transform.parent.GetComponent<AIBrain>();
    }

    public void SetWalkAnimation(bool value)
    {
        _animator.SetBool(_walkHash, value);
    }   

    public void SetIdleAnimation(bool value)
    {
        _animator.SetBool(_IdleHash, value);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity != 0);
        SetIdleAnimation(velocity == 0);
    }


    public void EnemyAttack()
    {
        if(_enemy.enemyType != EnemyType.ShieldEnemy)
        {
             int randAttack = UnityEngine.Random.Range(1, 5); //���� �⺻ : ����� = 3 : 1 ����
             _animator.SetInteger(_attackHash, randAttack);
        }
        else
        {
            _animator.SetTrigger(_attackHash);
        }
        //_animator.SetTrigger("canAttack");
        //_animator.SetBool("IsAttacking", true);
    }
    public void PlayAttackAnimation()
    {
        EnemyAttack();
    }

    public void SetEndOfAttackAnimation()
    {
        _brain.AIActionData.isAttack = false;
    }


    public void PlayDeadAnimation()
    {
        _animator.SetTrigger(_DeadTriggerHash);
        _animator.SetBool(_DeadBoolHash, true);
    }

    public void EndOfDeadAnimation()
    {
        _brain.Enemy.Die();
    }

}
