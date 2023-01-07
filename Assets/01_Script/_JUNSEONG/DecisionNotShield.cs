using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionNotShield : AIDecision
{
    [SerializeField]
    [Range(0.1f, 30f)] private float _distance = 5f;
    public float Distance { get => _distance; set => _distance = Mathf.Clamp(value, 0.1f, 30f); }

    public override bool MakeADecision()
    {
        float calc = Vector2.Distance(_brain.Target.position, transform.position);
        if (calc > _distance)
        {
            _aiActionData.isShield = false;
            _brain.GroundEnemyAnim.AttackBoolSet();
            return true;
        }
        else
        {
            if (_aiActionData.isShield == false)
            {
                _aiActionData.isShield = true;
            }
            return false;
        }
    }
}
