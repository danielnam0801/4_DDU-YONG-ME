using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    [SerializeField] private GameObject[] doorScript;
    [SerializeField] private bool stageClear;
    [SerializeField] private bool stagePlaying;

    Animator doorAnimator;

    private void Start()
    {
        doorScript[0] = transform.parent.gameObject;
        doorAnimator = doorScript[0].GetComponent<Animator>();
    }
    void Update()
    {
        DoorOpening();
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") && col.gameObject.name == "Player")
        {
            if (col.gameObject.GetComponent<GetKeyObject>().gettingKey > 0)
            {
                stageClear = true;
            }
            if (stageClear && !stagePlaying)
            {
                doorAnimator.SetBool("DoorOpen", true);
            }
        }
    }
    void DoorOpening()
    {
        if (doorAnimator.GetBool("DoorOpen"))
        {
            doorScript[0].transform.GetChild(0).gameObject.SetActive(true);
            doorScript[0].transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            doorScript[0].transform.GetChild(0).gameObject.SetActive(false);
            doorScript[0].transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}


