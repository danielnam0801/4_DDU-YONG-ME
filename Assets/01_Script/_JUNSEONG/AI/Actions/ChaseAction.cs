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

        Vector2 dir = (_brain.Target.position.x - transform.position.x > 0) ? new Vector2(1,0) : new Vector2(-1,0);

        _aiMovementData.direction = dir.normalized;
        _aiMovementData.pointOfInterest = _brain.Target.position;

        _brain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }

}
