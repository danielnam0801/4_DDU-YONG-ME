using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _layerMask2;
    [SerializeField] private LayerMask _layerMask3;
    [SerializeField] private float _boxX;
    [SerializeField] private float _boxY;
    [SerializeField] private float _jumppower;
    [SerializeField] private PlayerAnimatoinoControll _animControl;
    public float originSpeed;
    public float speed;
    public bool slimeShoes;
    [SerializeField] private bool Left;
    [SerializeField] private bool Right;
    [SerializeField] private BoxCollider2D _col;
    [SerializeField] private Collider2D _col2;
    [SerializeField] private RaycastHit2D downpan;
    [SerializeField] private float _originGravity;
    [SerializeField] private float _gravity;
    [SerializeField] private Vector2 _moveTo;
    [SerializeField] bool _canJump;
    [SerializeField] bool _knockBack;
    [SerializeField] Vector2 _knockBackDir;
    Rigidbody2D rigibody;

    public KeyCode JumpKey;

    public void JumpKiller(float cool)
    {
        StartCoroutine(JumpKill(cool));
    }
    private void Awake()
    {
        _col = GetComponentsInChildren<BoxCollider2D>()[1];
        _animControl = GetComponent<PlayerAnimatoinoControll>();
        rigibody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _moveTo.x = 0;
        rigibody.velocity = Vector2.zero;
        Jump(_jumppower);
        Move(Input.GetAxisRaw("Horizontal"));
        if (slimeShoes)
        {
            Collider2D _collider = GetComponent<Collider2D>();
            RaycastHit2D left1 = Physics2D.BoxCast(_collider.bounds.center+Vector3.left*0.5f, _collider.bounds.size * 0.5f, 0, Vector2.left, 0.1f, _layerMask);
            RaycastHit2D left2 = Physics2D.BoxCast(_collider.bounds.center + Vector3.left * 0.5f, _collider.bounds.size * 0.5f, 0, Vector2.left, 0.1f, _layerMask2);
            if (left1 || left2) Left = true; else Left = false;
            RaycastHit2D right = Physics2D.BoxCast(_collider.bounds.center + Vector3.right * 0.5f, _collider.bounds.size * 0.5f, 0, Vector2.right, 0.1f, _layerMask2);
            RaycastHit2D right2 = Physics2D.BoxCast(_collider.bounds.center + Vector3.right * 0.5f, _collider.bounds.size * 0.5f, 0, Vector2.right, 0.1f, _layerMask2);
            if (right2|| right) Right= true; else Right= false;

            if (!CheckGround() && (Left || Right))
            {
                _gravity = 0;
            }
            else _gravity = _originGravity;
        }
        if (Mathf.Abs(_moveTo.x) > 0)
        {
            _animControl.StartRun();
        }
        else _animControl.EndRun();
        if (_moveTo.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (_moveTo.x > 0) transform.localScale = new Vector3(1, 1, 1);
    }
    private void FixedUpdate()
    {
        if (_knockBack)
        {
            rigibody.velocity += _knockBackDir;
            if (_knockBackDir.y > 0) _knockBackDir.y -= Time.deltaTime * _gravity;
        }
        rigibody.velocity += _moveTo;
    }
    void Move(float x)
    {
        //rigibody.velocity = new Vector2(x * speed, rigibody.velocity.y);
        _moveTo.x = speed * x;

    }
    void Jump(float JumpPow)
    {
        if (DownJump())
        {

            if (Input.GetKeyDown
                (JumpKey) && Input.GetAxisRaw("Vertical") == -1)
            {
                StartCoroutine(DownStair());
                //rigibody.AddForce(new Vector2(0, -JumpPow), ForceMode2D.Impulse);
                KnockBack(new Vector2(0, -JumpPow));
                _moveTo += Vector2.down * JumpPow;
            }
        }
        if (CheckGround())
        {

            if (Input.GetKeyDown(JumpKey))
            {
                //rigibody.AddForce(new Vector2(0, JumpPow), ForceMode2D.Impulse);
                _moveTo += Vector2.up * JumpPow;
                JumpKiller(0.1f);
            }
        }
    }
    public void KnockBack(Vector2 dir)
    {
        _moveTo.y = 0;
        _knockBackDir = dir;
        StartCoroutine(KBCount());
    }
    IEnumerator KBCount()
    {
        _knockBack = true;
        yield return new WaitForSeconds(0.3f);
        _knockBack = false;
    }
    bool CheckGround()
    {
        if (_canJump)
        {

            if (Physics2D.BoxCast(_col.bounds.center, _col.size, 0f, Vector2.down, 0.1f, _layerMask) ||
                Physics2D.BoxCast(_col.bounds.center, _col.size, 0f, Vector2.down, 0.1f, _layerMask2) || 
                Physics2D.BoxCast(_col.bounds.center, _col.size, 0f, Vector2.down, 0.1f, _layerMask3))
            {
                _moveTo.y = 0;
                return true;
            }
            else
            {
                if (!Input.GetKey(JumpKey) && _moveTo.y > 0) _moveTo.y *= 0.5f;
                _moveTo.y -= _gravity * Time.deltaTime;
                //rigibody.velocity += Vector2.down * gravity * Time.deltaTime;
                return false;
            }
        }
        else return false;
    }
    bool DownJump()
    {
        downpan = Physics2D.BoxCast(_col.bounds.center, _col.size, 0f, Vector2.down, 0.1f, _layerMask2);
        if (downpan)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator DownStair()
    {
        _col2 = downpan.collider;
        _col2.isTrigger = true;
        yield return new WaitForSeconds(0.5f);
        _col2.isTrigger = false;
    }
    IEnumerator JumpKill(float cool)
    {
        _canJump = false;
        yield return new WaitForSeconds(0.1f);
        _canJump = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
    }
}