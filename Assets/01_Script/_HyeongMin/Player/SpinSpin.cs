using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSpin : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    Transform objTransform;
    float a;
    private void Start()
    {
        objTransform = GetComponent<Transform>();
    }
    void Update()
    {
        a += spinSpeed;
        objTransform.rotation = Quaternion.Euler(new Vector3(80, 0, a));
    }
}
