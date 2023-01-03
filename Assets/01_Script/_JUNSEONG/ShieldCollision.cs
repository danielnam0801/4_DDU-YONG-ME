using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    AIBrain _aiBrain;

    private void Awake()
    {
        _aiBrain = transform.parent.parent.GetComponent<AIBrain>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            collision.gameObject.GetComponent<Weapon>().state = Weapon.State.Item;
            collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down,ForceMode2D.Impulse);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 10f;
            
            //RaycastHit2D underHit = Physics2D.Raycast(collision.contacts[0].point, Vector2.down, 5f);
            //Debug.DrawRay(collision.contacts[0].point, Vector2.down * 5f);
            //collision.collider.gameObject.transform.position = underHit.point;
            
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("BackGround"))
        {
            _aiBrain.AIMovementData.direction.x = -_aiBrain.AIMovementData.direction.x;
            _aiBrain.AIMovementData.thinkTime = 0f;
        }
    }
}
