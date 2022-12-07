using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool canGrab;
    [SerializeField] private int    _playerLayer;
    [SerializeField] private int _weaponLayer; //각 맞는 레이어 넣어주셈ㅇㅇ
    Rigidbody2D _rigidbody;
    Collider2D _collider;
    public int cnt;
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
    }
    public void Grab()
    {
        state = State.Grab;
        _collider.isTrigger = true;
    }
    public void Shooting(Vector2 dir, float Power)
    {
        _rigidbody.AddForce(dir * Power, ForceMode2D.Impulse);
        state = State.Shoot;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.Shoot)
        {
            if (collision.gameObject.GetComponent<IsWall>())
            {
                state = State.Item;
                _collider.isTrigger = false;
            }
            if (collision.gameObject.GetComponent<EnemyOnHit>())
            {
                if(cnt > 0)
                {
                    cnt--;
                    collision.gameObject.GetComponent<EnemyOnHit>().HitOnWeapon();
                }
                else
                {
                    state = State.Item;
                    _collider.isTrigger = false;
                }
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
        }
        if (collision.gameObject.GetComponent<EnemyOnHit>())
        {
            state = State.Item;
            _collider.isTrigger = false;
        }

    }
}