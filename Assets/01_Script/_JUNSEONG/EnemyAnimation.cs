using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    protected readonly int _attackHash = Animator.StringToHash("Attack");
    protected readonly int _runHash = Animator.StringToHash("Run");
    protected readonly int _DeadBoolHash = Animator.StringToHash("Dead");
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
