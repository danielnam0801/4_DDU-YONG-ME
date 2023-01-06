using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _cam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
        }
    }
}
