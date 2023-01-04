using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    public Vector3[] Direction;
    public int Pattons;
    public GameObject PlayerObject;
    public bool Waitting;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPatton(int num, float wait)
    {
        Pattons = num;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Pattons == 0)
        {
            if (Waitting)
            {
                transform.position = PlayerObject.transform.position + (Vector3.up * 5);
            }
            else
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
        }
        if(Pattons == 1)
        {
            if (!Physics2D.Raycast(transform.position, Vector2.down, 0.3f)) transform.position += Vector3.down * speed * Time.deltaTime;
            
        }
    }
    IEnumerator Delay(int a)
    {
        Waitting = true;
        yield return new WaitForSeconds(a);
        Waitting = false;
    }
}
