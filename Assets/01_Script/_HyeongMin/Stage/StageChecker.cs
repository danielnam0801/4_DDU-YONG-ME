using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageChecker : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    PolygonCollider2D polygonCollider;
    [SerializeField] private string camname;
    private void Awake()
    {
        virtualCamera = GameObject.Find(camname).GetComponent<CinemachineVirtualCamera>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            virtualCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = polygonCollider;
            virtualCamera.Follow = gameObject.transform;
        }
    }
}

