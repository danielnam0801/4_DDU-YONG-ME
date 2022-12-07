using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _layerMask2;
    [SerializeField] private float _boxX;
    [SerializeField] private float _boxY;
    [SerializeField] private float jumppower;
    [SerializeField] private float speed;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private Collider2D col2;
    [SerializeField] private RaycastHit2D downpan;
    [SerializeField] private float gravity;
    Rigidbody2D rigibody;

    public KeyCode JumpKey;


    private void Awake()
    {
        col = GetComponentsInChildren<BoxCollider2D>()[1];
        rigibody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move(Input.GetAxisRaw("Horizontal"));
        Jump(jumppower);
    }
    void Move(float x)
    {
        rigibody.velocity = new Vector2(x * speed, rigibody.velocity.y);
    }
    void Jump(float JumpPow)
    {
        if (DownJump())
        {

            if (Input.GetKeyDown(JumpKey) && Input.GetAxisRaw("Vertical") == -1)
            {
                StartCoroutine(DownStair());
                rigibody.AddForce(new Vector2(0, -JumpPow), ForceMode2D.Impulse);
            }
        }
        if (CheckGround())
        {

            if (Input.GetKeyDown(JumpKey))
            {
                rigibody.AddForce(new Vector2(0, JumpPow), ForceMode2D.Impulse);
            }
        }
    }
    bool CheckGround()
    {
        if(Physics2D.BoxCast(col.bounds.center, col.size, 0f, Vector2.down, 0.1f, _layerMask)||
            Physics2D.BoxCast(col.bounds.center, col.size, 0f, Vector2.down, 0.1f, _layerMask2))
        {
            return true;
        }
        else
        {
            rigibody.velocity += Vector2.down * gravity * Time.deltaTime;
            return false;
        }
    }
    bool DownJump()
    {
        downpan = Physics2D.BoxCast(col.bounds.center, col.size, 0f, Vector2.down, 0.1f, _layerMask2);
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
        col2= downpan.collider;
        col2.isTrigger = true;
        yield return new WaitForSeconds(0.5f);
        col2.isTrigger = false;
    }
}
