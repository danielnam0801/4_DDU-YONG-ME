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

    [Header("백무빙 수치")]
    [SerializeField] private float BackMovingTime = 3f;
    [SerializeField] private float BackAfterMovingTime = 3f;
    [SerializeField] private float AfterMovingTime = 3f;

    [Header("속도")]
    [SerializeField] private float speed;

    [Header("속도 조작 속성")]
    [SerializeField] private float XSpeedValueControl = 3;
    [SerializeField] 
    [Range(0.0f,1f)] private float YSpeedValueControl = 0.1f;

    [SerializeField] 
    [Range(0,10)] private float speedRelativeControl;


    // 숫자 높아질수록 움직임 폭 완화됨
    // 무리함수 시작점 지정해줌

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
                if (!isMoving) // 움직이는 중이 아니면
                {
                    EnemyMoving(transform.position);
                    isMoving = true;    
                }
            }
        }
        else
        {
            //speed = 0; // 현재는 그냥 가만히 있는거로
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
    
    IEnumerator Moving(Vector2 InputTrans)// 호출된 시점에 위치값
    {

        if (transform.localScale.x == 1)
        { //플레이어가 오른쪽에 있을 때
            float x = 0; 
            while (x <= BackMovingTime) // 점점 왼쪽으로 가야함 /// transform.position 이 계속 바껴서 들어가지는지 잘 모르겠음
            {
                // 0, 0 을 기준으로 사용 
                float x1 = XSpeedValueControl * (-Mathf.Sqrt(x + speedRelativeControl) + Mathf.Sqrt(speedRelativeControl)); // 제 3분면쪽으로 꺽는 무리함수
                float y1 = Mathf.Pow(x, 2) * YSpeedValueControl; // 위 함수의 역함수
                transform.position = new Vector3(x1 + InputTrans.x, y1 + InputTrans.y, 0);
                x += Time.deltaTime;
                yield return null;
            }

            x = 0;
            Vector2 MovingAfterPos = transform.position;// 첫번째 빽무빙 수행후 끝날때 위치 저장
            while (x <= AfterMovingTime)
            {
                // 0, 0 을 기준으로 사용 
                float x1 = Mathf.Pow(x, 2) * XSpeedValueControl; //제 1분면쪽으로 꺽는 무리함수
                float y1 =  YSpeedValueControl * (Mathf.Sqrt(x + speedRelativeControl) - Mathf.Sqrt(speedRelativeControl)); // 위 함수의 역함수
                transform.position = new Vector3( MovingAfterPos.x + x1 , MovingAfterPos.y - y1, 0);
                x += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            Debug.Log("이동끝");
            isMoving = false;
        }
    }

    IEnumerator ShootingWaiter()
    {
        
        yield return new WaitForSeconds(_enemy.AttackDelay());
        isCanAttack = true;
    }

}
