using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool godSpear;
    public bool canGrab;
    public int counter;
    public float damage;
    public int counter_origin;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private int    _enemyLayer;
    [SerializeField] private int _weaponLayer; //각 맞는 레이어 넣어주셈ㅇㅇ
    Rigidbody2D _rigidbody;
    Collider2D _collider;
    public enum State
    {
        Grab,
        Shoot,
        Item
    }
    public State state = State.Item;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        //_weaponLayer = LayerMask.NameToLayer("Weapon");
        //_playerLayer = LayerMask.NameToLayer("Player");
        _weaponLayer = gameObject.layer;
    }
    void Update()
    {
        if (state == State.Item)
        {
            _rigidbody.velocity = Vector2.zero;
            _collider.isTrigger = false;

        }
        if (godSpear)
        {
            if (Input.GetMouseButtonDown(1))
            {
                transform.position = _playerObject.transform.position;
                state = State.Item;
            }
        }
    }
    public void Grab()
    {
        state = State.Grab;
        _collider.isTrigger = true;
        counter = counter_origin;
        Physics2D.IgnoreLayerCollision(_weaponLayer, _enemyLayer, false);
        _rigidbody.gravityScale = 0f;


    }
    public void Shooting(Vector2 dir, float Power, float Damage)
    {
        damage = Damage;
        _rigidbody.AddForce(dir * Power, ForceMode2D.Impulse);
        state = State.Shoot;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Shoot)
        {
            if (collision.gameObject.GetComponentInParent<EnemyOnHit>())
            {
                if(counter > 0)
                {
                    counter--;
                }
                else
                {
                    state = State.Item;
                    _collider.isTrigger = false;
                    Physics2D.IgnoreLayerCollision(_weaponLayer, _enemyLayer, true);
                    _rigidbody.gravityScale = 5f;
                }
            }
            if (collision.gameObject.GetComponent<IsWall>())
            {
                state = State.Item;
                _collider.isTrigger = false;
                Physics2D.IgnoreLayerCollision(_weaponLayer, _enemyLayer, true);
                _rigidbody.gravityScale = 5f;

            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (state == State.Shoot)
        {
            if (collision.gameObject.GetComponent<IsWall>())
            {
                state = State.Item;
                _collider.isTrigger = false;
            }
        //if (collision.gameObject.GetComponentInParent<EnemyOnHit>())
        //{
        //    if (counter > 0)
        //    {
        //        Debug.LogError("ASdf");
        //        collision.gameObject.GetComponentInParent<EnemyOnHit>().WeaponHit();
        //        counter--;
        //    }
        //    else
        //    {
        //        state = State.Item;
        //        _collider.isTrigger = false;

        //    }
        //}
        }
    
    }
}