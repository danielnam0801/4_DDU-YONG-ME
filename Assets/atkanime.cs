using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atkanime : MonoBehaviour
{
     Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SHOT()
    {
       
            animator = GetComponent<Animator>();
            //animator.SetTrigger("ATK");
        
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            //animator.SetTrigger("ATK");
            Invoke("Soff", 0.5f);
            
        }
    }
    void Soff()
    {
        //animator.SetTrigger("ATK");
    }
}
