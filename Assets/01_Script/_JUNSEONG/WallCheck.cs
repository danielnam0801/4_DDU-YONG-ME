using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    AIMovementData _movement;
    public LayerMask wallLayer;

    private void Awake()
    {
        _movement = transform.parent.Find("AI").GetComponent<AIMovementData>();
    }

    private void Update()
    {
        RaycastHit2D sideWalkCheck = Physics2D.Raycast(transform.position, new Vector3(_movement.direction.x, 0, 0), 1.5f, wallLayer);
        if (sideWalkCheck.collider != null)
        {
            _movement.direction.x = -_movement.direction.x;
            _movement.thinkTime = 0;
        }
    }
}
