using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    [SerializeField] private Door[] doorScript;
    [SerializeField] private bool stageClear;
    [SerializeField] private bool stagePlaying;
    void OnTriggerStay2D(Collider2D col)
    {
        if (stageClear && !stagePlaying)
        {
            if (col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
            doorScript[0].doorOpen = true;
            doorScript[0].doorClose = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("Player")) { return; }
        doorScript[0].doorClose = true;
        doorScript[0].doorOpen = false;
    }
}

