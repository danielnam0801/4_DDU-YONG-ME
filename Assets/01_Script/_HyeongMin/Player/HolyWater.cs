    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
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
