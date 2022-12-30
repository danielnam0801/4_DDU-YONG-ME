using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementKeyPress;
    
    [SerializeField]
    private AIState _currentState;

    [SerializeField]
    private Transform _target;
    public Transform Target
    {
        get => _target;
    }

    protected void Update()
    {
        if(_target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
            return;
        }
        else
        {
            _currentState.UpdateState();
        }
    }

    public void ChangeState(AIState state)
    {
        _currentState = state;
    }

    public void Move(Vector2 speed, Vector3 pos)
    {

    }
}
