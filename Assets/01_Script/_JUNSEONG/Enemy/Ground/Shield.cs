using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public string WeaponLayer = "Weapon";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(WeaponLayer))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
