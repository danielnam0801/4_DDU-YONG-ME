    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    [SerializeField] private float maxTime;
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            GetComponent<Animator>().SetBool("HolyWaterEnd", true);
            timer = 0;
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.GetComponent<TestEnemy>())
        {
            Debug.LogError("Holy true");
            TestEnemy enemyMovement = col.GetComponent<TestEnemy>();
            Destroy(col.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<TestEnemy>())
        {
            Debug.LogError("Holy false");
            TestEnemy enemyMovement = col.GetComponent<TestEnemy>();
            Destroy(col.gameObject);
        }
    }
}
