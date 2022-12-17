using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Shooting shooting;
    Rigidbody2d rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2d>();
    }

    
}
