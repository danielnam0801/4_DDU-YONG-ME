using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyGrail : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<TestEnemy>())
        {
            Debug.LogError("grail true");
            TestEnemy enemyMovement = col.GetComponent<TestEnemy>();
            enemyMovement.holyGrailSpeedSlow = true;
            StartCoroutine(HolyGrailAbility(col));
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<TestEnemy>())
        {
            Debug.LogError("grail false");
            TestEnemy enemyMovement = col.GetComponent<TestEnemy>();
            enemyMovement.holyGrailSpeedSlow = false;
        }
    }
    IEnumerator HolyGrailAbility(Collider2D col)
    {
        TestEnemy enemyMovement = col.GetComponent<TestEnemy>();
        float timer = 0;
        while (enemyMovement.holyGrailSpeedSlow)
        {
            timer += 0.15f;
            yield return new WaitForSeconds(0.1f);
            if (timer >= 3)
            {
                Debug.Log("OmaewaMoShindeiru");
                Destroy(col.gameObject);
                enemyMovement.holyGrailSpeedSlow = false;
            }
        }
        yield return null;
    }
}
