using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckDecision : AIDecision
{
    public LayerMask wallLayer;

    public override bool MakeADecision()
    {
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, GameManager.instance.Target.position, 15, wallLayer);
        if (wallCheck.collider == null)
            return true;
        else 
            return false;
    }

}
