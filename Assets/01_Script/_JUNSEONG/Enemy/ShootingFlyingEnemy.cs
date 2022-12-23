using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFlyingEnemy : EnemyBase
{
    Shooting shooting;
    Rigidbody2D rb;
    [SerializeField] bool isCanAttack = true;
    [SerializeField] bool isMoving = false;

    [Header("�鹫�� ��ġ")]
    [SerializeField] private float BackMovingTime = 3f;
    [SerializeField] private float BackAfterMovingTime = 3f;
    [SerializeField] private float AfterMovingTime = 3f;

    [Header("�ӵ�")]
    [SerializeField] private float speed;

    [Header("�ӵ� ���� �Ӽ�")]
    [SerializeField] private float XSpeedValueControl = 3;
    [SerializeField] 
    [Range(0.0f,1f)] private float YSpeedValueControl = 0.1f;

    [SerializeField] 
    [Range(0,10)] private float speedRelativeControl;


    // ���� ���������� ������ �� ��ȭ��
    // �����Լ� ������ ��������

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        shooting = transform.GetComponentInChildren<Shooting>();
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
            //speed = 0; // ����� �׳� ������ �ִ°ŷ�
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

        if (transform.localScale.x == 1)
        { //�÷��̾ �����ʿ� ���� ��
            float x = 0; 
            while (x <= BackMovingTime) // ���� �������� ������ /// transform.position �� ��� �ٲ��� �������� �� �𸣰���
            {
                // 0, 0 �� �������� ��� 
                float x1 = XSpeedValueControl * (-Mathf.Sqrt(x + speedRelativeControl) + Mathf.Sqrt(speedRelativeControl)); // �� 3�и������� ���� �����Լ�
                float y1 = Mathf.Pow(x, 2) * YSpeedValueControl; // �� �Լ��� ���Լ�
                transform.position = new Vector3(x1 + InputTrans.x, y1 + InputTrans.y, 0);
                x += Time.deltaTime;
                yield return null;
            }

            x = 0;
            Vector2 MovingAfterPos = transform.position;// ù��° ������ ������ ������ ��ġ ����
            while (x <= AfterMovingTime)
            {
                // 0, 0 �� �������� ��� 
                float x1 = Mathf.Pow(x, 2) * XSpeedValueControl; //�� 1�и������� ���� �����Լ�
                float y1 =  YSpeedValueControl * (Mathf.Sqrt(x + speedRelativeControl) - Mathf.Sqrt(speedRelativeControl)); // �� �Լ��� ���Լ�
                transform.position = new Vector3( MovingAfterPos.x + x1 , MovingAfterPos.y - y1, 0);
                x += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            Debug.Log("�̵���");
            isMoving = false;
        }
    }

    IEnumerator ShootingWaiter()
    {
        
        yield return new WaitForSeconds(_enemy.AttackDelay());
        isCanAttack = true;
    }

}
