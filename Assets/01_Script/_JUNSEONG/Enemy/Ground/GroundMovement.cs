using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundMovement : MonoBehaviour
{
    Enemy _enemy;

    private Rigidbody2D rb;

    private int nextMove = 0;
 
    public UnityEvent<float> onVelocityChange;

    protected float _currentVelocity = 0;
    protected Vector2 _movementdirection;

    protected virtual void Awake()
    { 
        _enemy = transform.GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    public int NextMove()
    {
        _currentVelocity = Random.Range(-1, 2);
        StartCoroutine("EnemyThink");
        return nextMove;
    }

    public void MoveAgent(Vector2 moveInput)
    {
        _movementdirection = moveInput.normalized;
    }


    private void FixedUpdate()
    {
        onVelocityChange?.Invoke(_currentVelocity);
        rb.velocity = _movementdirection * _currentVelocity;
    }

    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        rb.velocity = Vector2.zero;
    }
}
