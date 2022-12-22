using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 dir;
    [SerializeField]private int damage;

    // Update is called once per frame
    void Update()
    {
        transform.position += dir.normalized * Time.deltaTime * 4 ;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHP>())
        {
            if (collision.gameObject.GetComponent<PlayerHP>().stCross > 0)
            {
                collision.gameObject.GetComponent<PlayerHP>().Damage(damage);
                collision.gameObject.GetComponent<PlayerHP>().stCross--;

            }

            Destroy(gameObject);
        }
    }
}
