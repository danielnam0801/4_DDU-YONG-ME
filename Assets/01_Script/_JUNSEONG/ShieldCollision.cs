using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldCollision : MonoBehaviour
{
    AIBrain _aiBrain;
    CapsuleCollider2D _capsuleCollider;// enemy��ü collider
    BoxCollider2D _boxCollider; // shield collider
    private ShieldData _shieldData;
    public ShieldData ShieldData => _shieldData;

    public UnityEvent OnHit;

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
        if (collision.GetComponent<Weapon>())
        {
            if(collision.GetComponent<Weapon>().state == Weapon.State.Shoot)
            {
                if (_shieldData.attackTowardsTheShield == true)
                {
                    OnHit?.Invoke();
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == Define.Floor)
        //{
        //    _aiBrain.AIMovementData.direction.x = -_aiBrain.AIMovementData.direction.x;
        //    _aiBrain.AIMovementData.thinkTime = 0f;
        //}
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
}
