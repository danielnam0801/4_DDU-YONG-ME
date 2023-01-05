using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Enemy : MonoBehaviour, IHitable, IAgent
{
    [SerializeField] EnemySO _enemyData;
    public EnemySO EnemyData { get => _enemyData; }
    public bool IsEnemy => true;
    public Vector3 HitPoint { get; set; }

    [field: SerializeField]
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
    protected GroundEnemyAnim _enemyAnim;
    
    protected EnemyDebuffData enemyDebuff;
    public EnemyDebuffData EnemyDebuffData => enemyDebuff;
    
    
    protected virtual void Awake()
    {
        _brain = GetComponent<AIBrain>();
        _attack = GetComponent<EnemyAttack>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();  
        _groundMovement = GetComponent<GroundMovement>();
        _spriteRenderer = transform.Find("VisualSprite").GetComponent<SpriteRenderer>();
        enemyDebuff = transform.Find("AI").GetComponent<EnemyDebuffData>();
        _enemyAnim = transform.GetComponentInChildren<GroundEnemyAnim>();
        _isActive = true;
        if(_spriteRenderer.material.HasProperty("_Dissolve")){
            _spriteRenderer.material.SetFloat("_Dissolve", 1);
        }
  
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
        Debug.Log("PlayerÇÑÅ× ¸Â¾ÒÂÇ¿°");
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
        _enemyAnim.PlayDeadAnimation();
        OnDie?.Invoke();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void DieMaterial()
    {
        Sequence seq = DOTween.Sequence();
        Tween dissolve = DOTween.To(
            () => _spriteRenderer.material.GetFloat("_Dissolve"),
            x => _spriteRenderer.material.SetFloat("_Dissolve", x),
            0f,
            1f);

        seq.Append(dissolve);
        //seq.AppendCallback(() => Die());
    }
}
