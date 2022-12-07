using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemySO _enemy;
    protected CapsuleCollider2D _enemyCollider;// enemy모양따라 달라질예정
    public Animator _animator;
    protected Transform _transform;
    protected Transform _target;

    protected virtual void Awake()
    {
        _enemyCollider = GetComponent<CapsuleCollider2D>();
        _transform = GetComponent<Transform>();
        //if (_enemy.enemyType == EnemyType.FlyingEnemy)
        //{
        //    _animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        //}
        _animator = GetComponentInChildren<Animator>();
        _target = GameObject.Find("player").GetComponent<Transform>();
    }
}
