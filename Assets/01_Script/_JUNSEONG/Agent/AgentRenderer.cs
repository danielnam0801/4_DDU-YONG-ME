using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AgentRenderer : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    AIMovementData _movementData;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChaseAttackFaceDirection(Vector2 pointerInput)
    {
        Vector3 direction = (Vector3)pointerInput - transform.position;
        transform.localScale = (direction.x > 0) ? new Vector3(1,1,1) : new Vector3(-1, 1, 1);
    }

    public void IdleFaceDirection(Vector2 currentDir, Vector2 beforeDir)
    {
        if(currentDir.x == 0)
            transform.localScale = (beforeDir.x > 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); //������ �Ͼ�� ���� ���� ���⿡ ���� ���̽� �𷺼�
        else
            transform.localScale = (currentDir.x > 0) ? new Vector3(1,1,1) : new Vector3(-1, 1, 1);
        
    }
}