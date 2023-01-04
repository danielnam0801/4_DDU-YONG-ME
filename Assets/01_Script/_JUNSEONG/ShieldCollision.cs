using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldCollision : MonoBehaviour
{
    AIBrain _aiBrain;
    CapsuleCollider2D _capsuleCollider;// enemy��ü collider
    BoxCollider2D _boxCollider; // shield collider
    ShieldData _shieldData;

    private void Awake()
    {
        _aiBrain = transform.parent.parent.GetComponent<AIBrain>(); 
        _capsuleCollider = transform.parent.parent.GetComponent<CapsuleCollider2D>(); // ENemy������ �ݶ��̴� ã��
        _boxCollider = transform.GetComponent<BoxCollider2D>(); // ENemy������ �ݶ��̴� ã��
        _shieldData = transform.parent.parent.Find("AI").GetComponent<ShieldData>();
    }

    private void Update()
    {
        CanDamageCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            if(_shieldData.attackTowardsTheShield == false)
            {
                if (collision.GetComponent<Weapon>())
                {
                    _aiBrain.Enemy.GetHit(collision.gameObject.GetComponent<Weapon>().damage, collision.gameObject);
                    collision.gameObject.GetComponent<Weapon>().state = Weapon.State.Item;
                    Debug.Log("ONHIT");
                    collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
                    CollisionRigid(collision);
                }
            }
            else {
                if (collision.GetComponent<Weapon>())
                {
                    collision.gameObject.GetComponent<Weapon>().state = Weapon.State.Item;
                    Debug.Log("ONHIT");
                    collision.gameObject.GetComponent<Collider2D>().isTrigger = false;
                    CollisionRigid(collision);
                }
            }
            //EnemyColliderDisAble(collision);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BackGround"))
        {
            _aiBrain.AIMovementData.direction.x = -_aiBrain.AIMovementData.direction.x;
            _aiBrain.AIMovementData.thinkTime = 0f;
        }
    }

    private void CanDamageCheck()
    {
        if (_capsuleCollider.bounds.center.x - _boxCollider.bounds.center.x > 0) // ������ ������
        {
            if (GameManager.instance.Target.position.x - transform.position.x > 0) // �÷��̾ ������
            {
                _shieldData.attackTowardsTheShield = false;
            }
            else
            {
                _shieldData.attackTowardsTheShield = true;
            }
        }
        else // �������� ������
        {
            if (GameManager.instance.Target.position.x - transform.position.x > 0) // �÷��̾ ������
            {
                _shieldData.attackTowardsTheShield = true;
            }
            else
            {
                _shieldData.attackTowardsTheShield = false;
            }
        }
    }

    public void CollisionRigid(Collider2D collision)
    {
        Rigidbody2D rb = collision.transform.GetComponentInParent<Rigidbody2D>();
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        //rb.gravityScale = 10f;


        //Vector3 incomingVector = collision.transform.position - _aiBrain.transform.position;
        //incomingVector = incomingVector.normalized;
        //// �浹�� ���� ���� ���͸� ���س���.
        //Vector3 normalVector = collision.contacts[0].normal;
        //// ���� ���Ϳ� �Ի纤���� �̿��Ͽ� �ݻ纤�͸� �˾Ƴ���.
        //Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector); //�ݻ簢
        //reflectVector = reflectVector.normalized;


        //rb.AddForce(Vector3.down + reflectVector, ForceMode2D.Impulse);
        rb.AddForce(Vector3.down, ForceMode2D.Impulse);

    }

    public void EnemyColliderDisAble(Collision2D collision)
    {
        StartCoroutine("EnemyBodyAble", collision);
    }

    IEnumerator EnemyBodyAble(Collision2D collision)
    {
        _capsuleCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        _capsuleCollider.enabled = true;
        
    }
}
