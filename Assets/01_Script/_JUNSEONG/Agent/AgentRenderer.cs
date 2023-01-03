using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AgentRenderer : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FaceDirection(Vector2 pointerInput)
    {
        Vector3 direction = (Vector3)pointerInput - transform.position;
        
        //transform.localScale = (dir.x > 0) ? new Vector3(1,1,1) : new Vector3(-1, 1, 1);
        transform.localScale = (direction.x > 0) ? new Vector3(1,1,1) : new Vector3(-1, 1, 1);

        
        //Vector3 result = Vector3.Cross(Vector2.up, direction);

        //_spriteRenderer.flipX = result.z > 0;
    }
}
