using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(int damage)
    {
        Debug.Log("AttackMEllee");
        if(_waitBeforeNextAttack == false)
        {
            _brain.AIActionData.isAttack = true;

            float range = _brain.Enemy.EnemyData.AttackRange();

            float distance = Vector2.Distance(transform.position, _brain.Target.position);

            if (distance < range)
            {
                //IHitable hit = _brain.Target.GetComponent<IHitable>();
                //hit?.GetHit(damage, gameObject);
            }
            AttackFeedBack?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());

        }   
    }
}

