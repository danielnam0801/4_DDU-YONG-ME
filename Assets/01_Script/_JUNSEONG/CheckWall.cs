using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWall : MonoBehaviour
{
    CapsuleCollider2D capsuleCollider;
    AIMovementData _moveData;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        _moveData = transform.parent.Find("AI").GetComponent<AIMovementData>();
    }

    private void Update()
    {
        PlatformCheck();   
    }

    private void PlatformCheck()
    {
        RaycastHit2D platformCheck = Physics2D.Raycast(capsuleCollider.bounds.center + new Vector3(_moveData.direction.x, 0 , 0 ), Vector2.down, 0.5f, Define.Floor | Define.PassingFloor);
        Debug.DrawRay(capsuleCollider.bounds.center + new Vector3(_moveData.direction.x, 0, 0), Vector2.down * 0.5f , Color.red);
        if(platformCheck.collider == null)
        {
            _moveData.direction.x = -_moveData.direction.x;
            _moveData.thinkTime = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == Define.Floor)
        {
            _moveData.direction.x = -_moveData.direction.x;
            _moveData.thinkTime = 0;
        }
    }
}
