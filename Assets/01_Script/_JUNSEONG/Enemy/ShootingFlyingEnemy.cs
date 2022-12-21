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

    [Header("�鹫�� ��ġ")]
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
                if (!isMoving) // �����̴� ���� �ƴϸ�
                {
                    EnemyMoving(transform.position);
                    isMoving = true;    
                }
            }
        }
        else
        {
            speed = 0; // ����� �׳� ������ �ִ°ŷ�
        }

        //rb.velocity = new Vector2(dirX, dirY) * speed;
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

    private void EnemyMoving(Vector2 InputTrans)
    {
        StartCoroutine(Moving(InputTrans));
    }

    IEnumerator Moving(Vector2 InputTrans)// ȣ��� ������ ��ġ��
    {

        if (transform.localScale.x == 1) { //�÷��̾ �����ʿ� ���� ��
            int i = 0;
            while (transform.position.x >= InputTrans.x - BackMovingDistance) // ���� �������� ������ /// transform.position �� ��� �ٲ��� �������� �� �𸣰���
            {
                float x = Mathf.Pow(transform.position.y,2);
                float y = -Mathf.Sqrt(InputTrans.x);


                transform.position.x = Mathf.Lerp(InputTrans.x, InputTrans.x - BackMovingDistance, );
                Mathf.Lerp(transform.position.y, transform.position.y + 1, );
                i++;
            }
        }
        else // �÷��̾ ���ʿ� ���� ��
        {
            while(transform.position.x <= InputTrans.x + BackMovingDistance) // ���� ���������� ������
            {

            }
        }

        yield return new WaitUntil(() =>
    }

    IEnumerator ShootingWaiter()
    {
        
        yield return new WaitForSeconds(_enemy.AttackDelay());
        isCanAttack = true;
    }

}
