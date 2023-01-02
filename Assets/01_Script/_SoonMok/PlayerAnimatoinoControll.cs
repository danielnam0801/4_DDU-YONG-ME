using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatoinoControll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.Find("UnitRoot").GetComponent<Animator>();
    }

    public void StartRun()
    {
        animator.SetBool("Run", true);
    }
    public void EndRun()
    {
        animator.SetBool("Run", false);
    }
    public void StartAttack()
    {
        StopAllCoroutines();
        animator.SetBool("OnAttack", true);
        StartCoroutine(EndAttack());
    }
    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("OnAttack", false);
    }
}
