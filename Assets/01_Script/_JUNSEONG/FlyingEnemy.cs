using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{

    public float speed, circleRadius, lineOfSite, attackLineOfSite;

    private Rigidbody2D EnemyRB;
    public GameObject rightCheck, roofCheck, groundCheck;
    public LayerMask groundLayer;
    private bool facingRight = true, groundTouch, roofTouch, rightTouch;
    public float dirX = 1, dirY = 0.25f;

    public bool _isAttacking = false;
    public bool _isChasing = false;
    public bool _isCanChase;
    public bool _isCanAttack;
    public bool endAttack = true;
    public bool attackWaiting = false;

    FlyingEnemyAnim anim;

    private void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
        lineOfSite = _enemy.DetectRange();
        attackLineOfSite = _enemy.AttackRange();
        anim = transform.GetChild(0).GetComponent<FlyingEnemyAnim>();// ����� ��������Ʈ�� �� ��ũ��Ʈ�� �ִ� �� �ٷ� �ؿ� �����ؾ���
        //speed = _enemy.BeforeDetectSpeed();
    }

    private void Update()
    {
        HitDetection();
        if (!_isChasing)
        {
            EnemyRB.velocity = new Vector2 (dirX, dirY) * speed * Time.deltaTime;
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, _target.position, speed * Time.deltaTime);
        }
        
        if(_enemy.enemyType == EnemyType.FlyingAttack)
        {
            DetectPlayer();
        }
    }

    void HitDetection()
    {
        rightTouch = Physics2D.OverlapCircle(rightCheck.transform.position, circleRadius, groundLayer);
        roofTouch = Physics2D.OverlapCircle (roofCheck.transform.position, circleRadius, groundLayer);
        groundTouch = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
        HitLogic();   
    }

    private void HitLogic()
    {
        if(rightTouch && facingRight)
        {
            Flip();
        }
        else if(rightTouch && !facingRight)
        {
            Flip();
        }
        if (roofTouch)
        {
            dirY = UnityEngine.Random.Range(-0.25f, -0.3f);
        }
        else if (groundTouch)
        {
            dirY = UnityEngine.Random.Range(0.25f, 0.35f);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        dirX = -dirX;   
    }

    float distanceFromPlayer;
    private void DetectPlayer()
    {
        distanceFromPlayer = Vector2.Distance(_target.transform.position, transform.position);
        if(distanceFromPlayer < lineOfSite)
        {
            if(distanceFromPlayer > attackLineOfSite && !_isAttacking) // attack���� �ٱ��ʿ� ������
            {
                _isChasing = true;
                speed = _enemy.AfterDetectSpeed();
                Debug.Log("�Ѿư�����");
                transform.position = Vector2.MoveTowards(this.transform.position, _target.position, speed * Time.deltaTime);
            }
            else //attack ���� �ȿ�������
            {
                if(_enemy.enemyType == EnemyType.OneShotFlying)
                {
                    //enemyHPManager.Hp = 0;
                }
                else
                {
                    if (_isCanAttack) //�������� �ƴҶ� / �����Ҽ� �ִ��� ���������Ѵ�
                    {
                        _isChasing = false;
                        speed = 0;
                        AttackPlayer();
                    }
                    else
                    {
                        speed = 0;
                    }
                }
            }
        }
        else
        {
            _isChasing = false;
            speed = _enemy.BeforeDetectSpeed();
        }
    }

    private void AttackPlayer()
    {
        anim.Attack();
    }

    public IEnumerator AttackCount()
    {
        endAttack = false;
        attackWaiting = true;
        Debug.Log("���� ��Ÿ�� �����");
        yield return new WaitForSeconds(_enemy.AttackDelay());
        _isCanAttack = true;
        attackWaiting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rightCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(roofCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, attackLineOfSite);
    }
}
