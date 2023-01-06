using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingFlyingEnemy : EnemyBase
{
    FlyingEnemyAnim anim;

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

    // 숫자 높아질수록 움직임 폭 완화됨
    [SerializeField] 
    [Range(0,10)] private float speedRelativeControl;

    public UnityEvent AttackFeedBack;

    public bool canDetectAttack = false;

    protected override void Awake()
    {
        base.Awake();
        anim = transform.GetComponentInChildren<FlyingEnemyAnim>();
    }

    void Update()
    {
        Flip();
        float distanceToPlayer = Vector2.Distance(transform.position, _target.transform.position);
        CanAttack();
        if (distanceToPlayer < _enemy.AttackRange())
        {

            if (isCanAttack && canDetectAttack)
            {
                anim.FlyingAttack();
                AttackFeedBack?.Invoke();
                StartCoroutine(ShootingWaiter());
                isCanAttack = false;
            }
            if (distanceToPlayer < _enemy.DetectRange())
            {
                if (!isMoving) // 움직이는 중이 아니면
                {
                    EnemyMoving(transform.position);
                    isMoving = true;
                }
            }
        }
    }

    private void CanAttack()
    {
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Enemy"));
        RaycastHit2D canChasePlayer = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, 15, ~(Define.Weapon | Define.Enemy)); // EnemyLayer을 제외한 모든 레이어를 감지
        Debug.Log("현재 레이 닿은 오브젝트 : ", canChasePlayer.collider);
        Debug.DrawRay(transform.position * 5, _target.position - transform.position);
        if (canChasePlayer.collider != null)
        {
            if (canChasePlayer.collider.CompareTag("Player"))// 가장 먼저 닿은 레이가 플레이어일때 // 추적할 수 있게 됨
            {
                Debug.Log("추적가능!");
                canDetectAttack = true; // 현재는 장애물이 사이에 있으면 추적이 그냥 끊기는 방식
            }
            else canDetectAttack = false;

        }
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
        float randomXValue = UnityEngine.Random.Range(-0.5f, 0.5f);
        float randomYValue = UnityEngine.Random.Range(-0.02f, +0.02f);
        float RBeforeMovingTime = UnityEngine.Random.Range(-0.5f, 0.5f);
        float RAfterMovingTime = UnityEngine.Random.Range(-0.5f, 0.5f);
        
        float XSpeedValueControl = this.XSpeedValueControl + randomXValue;
        float YSpeedValueControl = this.YSpeedValueControl + randomYValue;

        float BackMovingTime = this.BackMovingTime + RBeforeMovingTime;
        float AfterMovingTime = this.BackMovingTime + RAfterMovingTime;
        
        #region 플레이어가 오른쪽에 있을때 이동
        if (transform.localScale.x == 1)
        { //플레이어가 오른쪽에 있을 때
            float x = 0; 
            while (x <= BackMovingTime)
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
                float x1 = XSpeedValueControl * (Mathf.Sqrt(x + speedRelativeControl) - Mathf.Sqrt(speedRelativeControl)); //제 1분면쪽으로 꺽는 무리함수
                float y1 = Mathf.Pow(x, 2) * YSpeedValueControl; // 위 함수의 역함수
                transform.position = new Vector3( MovingAfterPos.x + x1, MovingAfterPos.y - y1, 0);
                x += Time.deltaTime;
                yield return null;
            }
            yield return null;

            Debug.Log("이동끝");
            isMoving = false;
        }
        #endregion
        #region 플레이어가 왼쪽에 있을때 이동
        else
        {
            float x = 0;
            while (x <= BackMovingTime)
            {
                // 0, 0 을 기준으로 사용 
                float x1 = -XSpeedValueControl * (-Mathf.Sqrt(x + speedRelativeControl) + Mathf.Sqrt(speedRelativeControl)); // 제 3분면쪽으로 꺽는 무리함수
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
                float x1 = -XSpeedValueControl * (Mathf.Sqrt(x + speedRelativeControl) - Mathf.Sqrt(speedRelativeControl)); //제 1분면쪽으로 꺽는 무리함수
                float y1 = Mathf.Pow(x, 2) * YSpeedValueControl; // 위 함수의 역함수
                transform.position = new Vector3(MovingAfterPos.x + x1, MovingAfterPos.y - y1, 0);
                x += Time.deltaTime;
                yield return null;
            }
            yield return null;

            Debug.Log("이동끝");
            isMoving = false;
        }
        #endregion
    }

    IEnumerator ShootingWaiter()
    {
        
        yield return new WaitForSeconds(_enemy.AttackDelay());
        isCanAttack = true;
    }

}
