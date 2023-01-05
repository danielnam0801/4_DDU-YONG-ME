using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundEnemyAnim : MonoBehaviour
{

    protected AIBrain _brain;

    protected readonly int _attackHash = Animator.StringToHash("Attack");
    protected readonly int _CanAttackHash = Animator.StringToHash("canAttack");

    protected readonly int _DeadTriggerHash = Animator.StringToHash("Death");
    protected readonly int _DeadBoolHash = Animator.StringToHash("IsDead");
    protected readonly int _walkHash = Animator.StringToHash("Walk");
    protected readonly int _IdleHash = Animator.StringToHash("Idle");

    Animator _animator;

    protected void Awake()
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
        if(_brain.Enemy.EnemyData.enemyType != EnemyType.ShieldEnemy)
        {
             int randAttack = UnityEngine.Random.Range(1, 5); //현재 기본 : 스페셜 = 3 : 1 비율
             _animator.SetInteger(_attackHash, randAttack);
            _animator.SetTrigger(_CanAttackHash);
        }
        else
        {
            _animator.SetTrigger(_attackHash);
        }
    }
    public void PlayAttackAnimation()
    {
        EnemyAttack();
        Debug.Log("AttackAnimPlay");
    }

    public void SetEndOfAttackAnimation()
    {
        StartCoroutine("TimeWait");
    }

    IEnumerator TimeWait()
    {
        yield return new WaitForSeconds(0.15f);
        _brain.AIActionData.isAttack = false;
    }


    public void PlayDeadAnimation()
    {
        _animator.SetTrigger(_DeadTriggerHash);
        _animator.SetBool(_DeadBoolHash, true);
    }

    public void EndOfDeadAnimation()
    {
        Debug.Log("죽음");
        _brain.Enemy.Die();
    }

}
