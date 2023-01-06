using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyGrail : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("GroundEnemy"))
        {
            StartCoroutine(HolyGrailAbility(col));
            //Debug.LogError("bible true");
            col.transform.Find("AI").GetComponent<EnemyDebuffData>().HolyGrailSlow = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.CompareTag("GroundEnemy"))
        {
            //Debug.LogError("bible true");
            col.transform.Find("AI").GetComponent<EnemyDebuffData>().HolyGrailSlow = true;
        }
    }
    IEnumerator HolyGrailAbility(Collider2D col)
    {
        TestEnemy enemyMovement = col.GetComponent<TestEnemy>();
        EnemyDebuffData enemy = col.transform.Find("AI").GetComponent<EnemyDebuffData>();
        float timer = 0;
        while (enemy.HolyGrailSlow)
        {
            timer += 0.15f;
            yield return new WaitForSeconds(0.1f);
            if (timer >= 3)
            {
                Debug.Log("OmaewaMoShindeiru");
                if (col.gameObject.CompareTag("GroundEnemy"))
                {
                    col.GetComponentInParent<Enemy>().GetHit(10, this.gameObject);
                }
                enemy.HolyGrailSlow = false;
            }
        }
        yield return null;
    }
}
