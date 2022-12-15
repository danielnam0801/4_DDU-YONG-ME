using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAnim : EnemyBase
{
    EnemyMovement enemyMove;
    EnemyAttack enemyAttack;

    protected override void Awake()
    {
       enemyMove = transform.parent.GetComponent<EnemyMovement>();
       enemyAttack = transform.parent.GetComponent<EnemyAttack>();
    }

    public void EnemeyAttack()
    {
        int randAttack = UnityEngine.Random.Range(1, 5); //���� �⺻ : ����� = 3 : 1 ����
        _animator.SetInteger("Attack", randAttack);
        _animator.SetTrigger("canAttack");
        _animator.SetBool("IsAttacking", true);
    }

    public void EndAttacking()
    {
        Debug.Log("������ �������ϴ�");
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
