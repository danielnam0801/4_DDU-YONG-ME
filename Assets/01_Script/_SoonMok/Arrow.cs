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
            collision.GetComponent<PlayerHP>().Damage(damage) ;
            Destroy(gameObject);
        }
    }
}
