using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected AIBrain _brain;

    public UnityEvent AttackFeedBack;
    private EnemyAttackData _enemyAttackData;

    protected virtual void Awake()
    {
        _brain = GetComponent<AIBrain>();
        _enemyAttackData = transform.Find("AI").GetComponent<EnemyAttackData>();
    }

    public abstract void Attack(int damage);

    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _enemyAttackData._waitBeforeNextAttack = true;
        yield return new WaitForSeconds(_enemyAttackData._attackDelay);
        _enemyAttackData._waitBeforeNextAttack = false;
    }


}
