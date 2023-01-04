using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterGlass : MonoBehaviour
{
    [SerializeField] private GameObject holyWaterObj;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(holyWaterObj,transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
