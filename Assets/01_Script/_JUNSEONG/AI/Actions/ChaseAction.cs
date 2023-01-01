using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        if (_aiActionData.isAttack)
        {
            _aiActionData.isAttack = false;
        }

        Vector2 dir = _brain.Target.position - transform.position;

        _aiMovementData.direction = dir.normalized;
        _aiMovementData.pointOfInterest = _brain.Target.position;

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }

}
