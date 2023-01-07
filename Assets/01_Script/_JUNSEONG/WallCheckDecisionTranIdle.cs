using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckDecisionTranIdle : AIDecision
{
    public override bool MakeADecision()
    {
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, GameManager.instance.Target.position, 15, Define.Floor);
        if (wallCheck.collider != null)
            return false;
        else
            return true;
    }
}
