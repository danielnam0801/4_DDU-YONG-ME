using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private bool isExplosion;
    [SerializeField] private int damage;
    [SerializeField] private bool isActivated;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Walk>() && !isActivated)
        {
            if (isExplosion) damage = collision.gameObject.GetComponent<PlayerHP>().MaxHP;

            StartCoroutine(TrapDelay());
            Vector2 dir = new Vector2(collision.gameObject.transform.position.x - transform.position.x, 3f * collision.gameObject.transform.position.y - transform.position.y > 0 ? 5 : -1); ;
            if (GetComponentInChildren<SenceSc>() && GetComponentInChildren<SenceSc>().item == SenceSc.Item.FakePlat) dir.y = 1;
            Debug.Log(dir.y);
            dir.Normalize();
            collision.gameObject.GetComponent<Walk>().JumpKiller(0.5f);
            collision.gameObject.GetComponent<Walk>().KnockBack(dir.normalized * 13);
            if (collision.gameObject.GetComponent<PlayerHP>().stCross > 0)
            {
                collision.gameObject.GetComponent<PlayerHP>().Damage(damage);
                collision.gameObject.GetComponent<PlayerHP>().stCross--;

            }
            Debug.Log("asdf;");

        }
    }
    IEnumerator TrapDelay()
    {
        isActivated = true;
        yield return new WaitForSeconds(0.5f);
        isActivated = false;
    }
}
