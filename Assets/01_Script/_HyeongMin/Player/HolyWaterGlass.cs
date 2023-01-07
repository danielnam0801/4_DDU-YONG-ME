using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterGlass : MonoBehaviour
{
    [SerializeField] private GameObject holyWaterObj;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Floor") || coll.gameObject.layer == LayerMask.NameToLayer("PassingFloor"))
        {
            Instantiate(holyWaterObj, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
