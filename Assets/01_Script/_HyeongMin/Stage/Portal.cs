using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject anotherPortal;
    [SerializeField] private KeyCode interactionKey;
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(interactionKey))
            {
                coll.transform.position = anotherPortal.transform.position;
            }
        }
    }
}
