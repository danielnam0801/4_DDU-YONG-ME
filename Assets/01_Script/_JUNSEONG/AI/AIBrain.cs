using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIBrain : MonoBehaviour
{
    public UnityEvent OnFireButtonPress;
    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged;
    
    [SerializeField]
    private AIState _currentState;

    [SerializeField]
    private Transform _target;
    public Transform Target
    {
        get => _target;
    }

    private AIActionData _aiActionData;
    public AIActionData AIActionData { get => _aiActionData; }

    private AIMovementData _aimovementData;
    public AIMovementData AIMovementData { get => _aimovementData; }

    protected virtual void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
        _aimovementData = transform.Find("AI").GetComponent<AIMovementData>();
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

    public void Move(Vector2 direction, Vector3 targetPos)
    {
        OnMovementKeyPress?.Invoke(direction);
        OnPointerPositionChanged?.Invoke(targetPos);
    }

    public virtual void Attack()
    {
        OnFireButtonPress?.Invoke();
    }
}
