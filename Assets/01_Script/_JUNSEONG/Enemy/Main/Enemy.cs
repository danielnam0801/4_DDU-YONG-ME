using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable, IAgent
{
    [SerializeField] EnemySO _enemyData;
    public EnemySO EnemyData { get => _enemyData; }
    public bool IsEnemy => true;
    public Vector3 HitPoint { get; set; }
    public float Health { get; set; }

    [field : SerializeField]
    public UnityEvent OnDie { get; set; }
    [field : SerializeField]
    public UnityEvent OnGetHit { get; set; }

    protected bool _isDead = false;

    [SerializeField] protected bool _isActive = false;
    protected AIBrain _brain;
    protected EnemyAttack _attack;
    protected GroundMovement _groundMovement;
    protected CapsuleCollider2D _bodyCollider;
    protected SpriteRenderer _spriteRenderer = null;
    
    protected virtual void Awake()
    {
        _brain = GetComponent<AIBrain>();
        _attack = GetComponent<EnemyAttack>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();  
        _groundMovement = GetComponent<GroundMovement>();
        _spriteRenderer = transform.Find("VisualSprite").GetComponent<SpriteRenderer>();
        _isActive = true;
        SetEnemyData();
    }

    private void SetEnemyData()
    {
        _attack.AttackDelay = _enemyData.AttackDelay();

        transform.Find("AI/IdleState/TranChase")
            .GetComponent<DecisionInner>().Distance = _enemyData.DetectRange();
        transform.Find("AI/ChaseState/TranIdle")
            .GetComponent<DecisionInner>().Distance = _enemyData.DetectRange();
        transform.Find("AI/ChaseState/TranAttack")
            .GetComponent<DecisionInner>().Distance = _enemyData.AttackRange();
        transform.Find("AI/AttackState/TranChase")
            .GetComponent<DecisionOuter>().Distance = _enemyData.AttackRange();

        Health = _enemyData.HP();
    }

    public virtual void PerformAttack()
    {
        if (_isDead == false && _isActive == true)
        {
            _attack.Attack(_enemyData.Damage());
        }
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_isDead == true) return;

        Health -= damage;
        HitPoint = damageDealer.transform.position;

        OnGetHit?.Invoke();

        if (Health <= 0)
            DeadProcess();
    }

    private void DeadProcess()
    {
        Health = 0;
        _isDead = true;
        OnDie?.Invoke();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
