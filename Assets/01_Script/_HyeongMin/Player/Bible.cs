using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("GroundEnemy"))
        {
            //Debug.LogError("bible true");
            col.transform.Find("AI").GetComponent<EnemyDebuffData>().bibleSpeedSlow = true;           
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("GroundEnemy"))
        {
            //Debug.LogError("bible true");
            col.transform.Find("AI").GetComponent<EnemyDebuffData>().bibleSpeedSlow = false;
        }
    }
}
