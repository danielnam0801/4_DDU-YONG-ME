using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StageChecker : MonoBehaviour
{
    [SerializeField] private DoorChecker doorChecker;
        CinemachineVirtualCamera virtualCamera;
    PolygonCollider2D polygonCollider;
    [SerializeField] private string camname;
    private void Awake()
    {
        virtualCamera = GameObject.Find(camname).GetComponent<CinemachineVirtualCamera>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        virtualCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = polygonCollider;
        virtualCamera.Follow = gameObject.transform;
    }
}
