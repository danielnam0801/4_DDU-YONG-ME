using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private AIBrain _brain = null;
    
    [SerializeField] private List<AIAction> _actions;
    [SerializeField] private List<AITransition> _transition = null;

    private void Awake()
    {
        _brain = transform.parent.parent.GetComponent<AIBrain>();
    }

    public void UpdateState()
    {
        foreach(AIAction action in _actions)
        {
            action.TakeAction();
        }
        
        foreach(AITransition t in _transition)
        {
            bool result = false;
            foreach(AIDecision d in t.decisions)
            {
                result = d.MakeADecision();
                if (!result)
                {
                    break;
                }
            }

            if (result == false)
            {
                if(t.positiveState != null)
                {
                    _brain.ChangeState(t.positiveState);
                    return;
                }
            }
            else
            {
                if(t.negativeState != null)
                {
                    _brain.ChangeState(t.negativeState);
                    return;
                }
            }
        }
    }


}
