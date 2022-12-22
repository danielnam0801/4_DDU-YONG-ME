using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<EnemyMovement>())
        {
            Debug.LogError("bible true");
            EnemyMovement enemyMovement = col.GetComponent<EnemyMovement>();
            //enemyMovement.bibleSpeedSlow = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<EnemyMovement>())
        {
            Debug.LogError("bible false");
            EnemyMovement enemyMovement = col.GetComponent<EnemyMovement>();
            //enemyMovement.bibleSpeedSlow = false;
        }
    }
}
