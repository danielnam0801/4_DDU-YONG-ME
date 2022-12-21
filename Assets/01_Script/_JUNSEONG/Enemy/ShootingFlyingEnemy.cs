using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFlyingEnemy : EnemyBase
{
    Shooting shooting;
    Rigidbody2D rb;
    [SerializeField] bool isCanAttack = true;
    [SerializeField] bool isMoving = true;

    [SerializeField] private float dirX = 1;
    [SerializeField] private float dirY = 0;

    [Header("백무빙 수치")]
    [SerializeField] private float BackMovingDistance = 3f;


    [SerializeField]
    private float speed;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        shooting = transform.GetComponentInChildren<Shooting>();
        speed = _enemy.BeforeDetectSpeed();   
    }

    void Update()
    {
        //Debug.Log($"Player is Null? {_target}", _target);
        Flip();
        float distanceFromPlayer = Vector2.Distance(transform.position, _target.transform.position);
        if(distanceFromPlayer < _enemy.AttackRange())
        {
            if (isCanAttack)
            {
                shooting.Shoot();
                StartCoroutine(ShootingWaiter());
                isCanAttack = false;
            }
            if (distanceFromPlayer < _enemy.DetectRange())
            {
                if (!isMoving) // 움직이는 중이 아니면
                {
                    EnemyMoving();
                    isMoving = true;    
                }
            }
        }
        else
        {
            speed = 0; // 현재는 그냥 가만히 있는거로
        }

        rb.velocity = new Vector2(dirX, dirY) * speed;
    }

    private void Flip()
    {
        if(_target.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
    }

    private void EnemyMoving()
    {
        StartCoroutine("Moving");
    }

    IEnumerator Moving()
    {
        dirX = -dirX;
        speed = _enemy.BeforeDetectSpeed();
        for (int i = 0; i < 10; i++)
        {
            if (i <= 5)
            {
                Mathf.Lerp(speed, _enemy.AfterDetectSpeed(), 0.5f * i); //속도 증가
            }
            else
            {
                Mathf.Lerp(speed, _enemy.BeforeDetectSpeed(), 0.5f);//속도 감소
            }
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        isMoving = false;
    }

    IEnumerator ShootingWaiter()
    {
        
        yield return new WaitForSeconds(_enemy.AttackDelay());
        isCanAttack = true;
    }

}
