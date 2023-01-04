using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageChecker : MonoBehaviour
{
    DoorChecker doorChecker;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") && col.gameObject.name == "Player")
        {
            col.gameObject.GetComponent<GetKeyObject>().gettingKey = false;
            //LoadScene;
        }
    }
}
