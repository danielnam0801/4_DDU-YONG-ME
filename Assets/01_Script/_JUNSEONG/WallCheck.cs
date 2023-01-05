using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    AIMovementData _movement;

    private void Awake()
    {
        _movement = transform.Find("AI").GetComponent<AIMovementData>();
    }

    private void Update()
    {
        RaycastHit2D sideWalkCheck = Physics2D.Raycast(transform.position, new Vector3(_movement.direction.x, 0, 0), 1.5f, Define.Floor);
        Debug.DrawRay(transform.position, new Vector3(_movement.direction.x,0,0) * 1.5f, Color.yellow);
        if (sideWalkCheck.collider != null)
        {
            _movement.direction.x = -_movement.direction.x;
            _movement.thinkTime = 0;
        }
    }
}
