using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Transform target;
    public Transform Target => target;

    private void Awake()
    {
        target = GameObject.Find("Player1").GetComponent<Transform>();
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    
    }
}
