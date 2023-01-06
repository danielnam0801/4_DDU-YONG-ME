using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageChecker : MonoBehaviour
{
    [SerializeField] private bool dontLoadScene;
    DoorChecker doorChecker;
    [SerializeField] private bool keyUsed;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!keyUsed)
            {
                col.gameObject.GetComponent<GetKeyObject>().gettingKey--;
                keyUsed = true;
            }
            //LoadScene;
        }
    }
}