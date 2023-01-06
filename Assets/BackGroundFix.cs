using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFix : MonoBehaviour
{
    private void Start()
    {
        transform.position = GameManager.instance.Target.transform.position + new Vector3(0.7f,0,0);
    }

    private void FixedUpdate()
    {
        transform.position = GameManager.instance.Target.transform.position;
    }
}
