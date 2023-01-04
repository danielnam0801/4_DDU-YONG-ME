using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSpin : MonoBehaviour
{
    Transform objTransform;
    int a;
    private void Start()
    {
        objTransform = GetComponent<Transform>();
    }
    void Update()
    {
        a += 1;
        objTransform.rotation = Quaternion.Euler(new Vector3(80, 0, a));
    }
}
