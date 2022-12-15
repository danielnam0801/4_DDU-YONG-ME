using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : EnemyBase
{
    public bool _isAttack = false;
    public bool _isAttacking = false;
    public bool canAttack = true;
    public bool _isAfterAttack = false;
    public bool endAttacking = true;
    public float _attackDelay = 0f;

    public int afterAttackMove = 0;

    public UnityEvent AttackFeedBack;

    EnemyMovement enemyMovement;
    GroundEnemyAnim anim;

    //[SerializeField]
    //Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        enemyMovement = GetComponent<EnemyMovement>();
        anim =  transform.GetComponentInChildren<GroundEnemyAnim>();
        //if(_enemy.enemyType == EnemyType.FlyingEnemy)
        //{
        //    _animator = GameObject.Find("VisualEnemy").GetComponent<Animator>();
        //}
    }

    private void Update()
    {
        Debug.Log($"공격중인가요 : {_isAttacking}");
        if (endAttacking)
        {
            _attackDelay -= Time.deltaTime;
        }
        if (_attackDelay < 0f) _attackDelay = 0;

        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= _enemy.AttackRange())
        {
            if (_enemy.enemyType == EnemyType.ShieldEnemy) enemyMovement.FaceTarget();
            enemyMovement.nextMove = 0;
            enemyMovement._isThinking = false;
            enemyMovement.nextMove = 0;
            afterAttackMove = (_target.position.x - transform.position.x < 0) ? -1 : 1;
            _isAttack = true;
            if (_attackDelay == 0)
            {
                EnemyAttacking();
                canAttack = true;
                _isAttacking = true;
                endAttacking = false;
                _isAfterAttack = true;

            }
            else
            {
                canAttack = false;
                _isAfterAttack = false;
                if (!_isAttacking)
                    enemyMovement.FaceTarget();
                Debug.Log("돌아간다아ㅏ아아아아아아ㅏ아앙");
            }

        }
        else if (distance > _enemy.AttackRange())
        {
            if(_enemy.enemyType == EnemyType.ShieldEnemy) _isAttacking=false;
            enemyMovement._isThinking = true;
            _isAttack = false;
            if (_isAfterAttack)
            {
                enemyMovement.nextMove = afterAttackMove;
                _isAfterAttack = false;
            }
        }
    }

    private void EnemyAttacking()
    {
        AttackFeedBack?.Invoke();// 공격시 효과같은거 넣어주기
        _attackDelay = _enemy.AttackDelay();
        anim.EnemeyAttack();
    }
}
