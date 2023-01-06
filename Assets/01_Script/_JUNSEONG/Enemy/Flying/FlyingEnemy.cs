using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    public float speed, circleRadius, lineOfSite, attackLineOfSite;

    private Rigidbody2D EnemyRB;
    public GameObject rightCheck, roofCheck, groundCheck;
    private LayerMask groundLayer;
    private bool facingRight = true, groundTouch, roofTouch, rightTouch;
    public float dirX = 1, dirY = 0.25f;

    [Header("Enemy상태")]
    public bool isCanChase = false;
    public bool isAttacking = false;
    public bool startAttack = false;
    public bool endAttack = false;
    public bool isCanAttack = true; // 공격 쿨타임이 끝났는지맘ㄴ 체크
    public bool isWaitingAttackCool = false;
    public bool isMoving = false;

    bool OneShotEnemyDetectPlayer = false;

    float distanceFromPlayer; // 플레이어와 나 사이 거리

    FlyingEnemyAnim anim;
    EnemyHPManager hpManager;

    public LayerMask HideVision;
    private void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
        lineOfSite = _enemy.DetectRange();
        attackLineOfSite = _enemy.AttackRange();
        anim = transform.GetChild(0).GetComponent<FlyingEnemyAnim>();// 비쥬얼 스프라이트는 이 스크립트가 있는 곳 바로 밑에 존재해야함
        hpManager = transform.GetComponent<EnemyHPManager>();
        groundLayer = Define.Floor;
        //if(_enemy.enemyType == EnemyType.FlyingAttack)
        //{
            
        //}
        //speed = _enemy.BeforeDetectSpeed();
    }

    private void Update()
    {
        distanceFromPlayer = Vector2.Distance(_target.transform.position, transform.position); // target과 나 사이 거리

        if (_enemy.enemyType == EnemyType.OneShotFlying)// 얘는 공격 한번이 끝임
        {
            if (isAttacking)
            {
                hpManager.HP = 0; // 공격이 시작되면 범위 안에 들어왔다는 뜻임으로 체력을 0으로 만들어서 터트림 
            }
        }

        if((_enemy.enemyType == EnemyType.FlyingAttack || _enemy.enemyType == EnemyType.OneShotFlying) && !isAttacking) //공격중이 아니고 
        {
            Debug.Log("OneShot FLying  " + gameObject.name);
            DetectPlayer();
        }

        anim.IsChasing(isCanChase && !isAttacking); // 추적이 가능하고 공격중이 아닌경우 true반환
        anim.IsIdle(!isCanChase && !isAttacking); //추적중이 아니거나 공격중이 아닐경우 true반환
        
        HitDetection(); // 벽과의 충돌 감지
      
        if (endAttack) StartCoroutine(AttackCount());
        #region 공격관련
        //if (isAttacking) speed = 0; // 공격중일때는 멈춘다
        if (!isCanChase) // 플레이어와 나(적) 사이에 벽이있거나 범위 밖인 경으
        {
            speed = _enemy.BeforeDetectSpeed();
        }
        else if(!isAttacking && isCanAttack)//범위 안에 있고 플레이어와 나 사이에 아무것도 없을경우 
        {
            if (CanAttackCheck())// 공격이 가능한지 체크
            {
                isCanAttack = false;
                startAttack = true;
                AttackPlayer();
                startAttack = false;
            }
            else
            {
                speed = _enemy.AfterDetectSpeed();
                transform.position = Vector2.MoveTowards(this.transform.position, _target.position, speed * Time.deltaTime);

            }
        }
        #endregion

        EnemyRB.velocity = new Vector2(dirX, dirY) * speed * Time.deltaTime;
        
    }


    private bool CanAttackCheck()
    { 
        Debug.Log("AttackCnt");
        return (distanceFromPlayer < attackLineOfSite); // target이 Attack 사정거리 안에있고 공격이 가능한 상태일때 true를 반환
    }

    #region 스프라이트 뒤집기
    private void PlayerPosCheck()
    {
        if(transform.position.x >= _target.position.x)
            transform.Rotate(new Vector3(0, 180, 0));
        else
            transform.Rotate(new Vector3(0, 0, 0));

    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        dirX = -dirX;   
    }
    #endregion
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


    private void DetectPlayer() // 플레이어 감지
    {
        if (distanceFromPlayer < lineOfSite)
        {
            int layerMask = (-1) - (1 << LayerMask.NameToLayer("Enemy"));
            RaycastHit2D canChasePlayer = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, 15 ,HideVision);; // EnemyLayer을 제외한 모든 레이어를 감지
            Debug.Log("현재 레이 닿은 오브젝트 : ", canChasePlayer.collider);
            Debug.DrawRay(transform.position * 5, _target.position - transform.position);
            if (canChasePlayer.collider.CompareTag("Player"))// 가장 먼저 닿은 레이가 플레이어일때 // 추적할 수 있게 됨
            {
                Debug.Log("추적가능!");
                isCanChase = true; // 현재는 장애물이 사이에 있으면 추적이 그냥 끊기는 방식
            }
            else isCanChase = false;
            
            if(_enemy.enemyType == EnemyType.OneShotFlying) // Trigger 활성화 한번만 되게 하는 코드
            {
                if (OneShotEnemyDetectPlayer == false)
                {
                    anim.DetectPlayer();
                    OneShotEnemyDetectPlayer = true;
                }
            }
            #region 바꾸기 전
            //if(_enemy.enemyType == EnemyType.OneShotFlying)
            //PlayerPosCheck();
            //if (distanceFromPlayer > attackLineOfSite && !_isAttacking) // attack범위 바깥쪽에 있을때
            //{
            //    _isChasing = true;
            //    speed = _enemy.AfterDetectSpeed();
            //    Debug.Log("쫓아가는중");
            //    transform.position = Vector2.MoveTowards(this.transform.position, _target.position, speed * Time.deltaTime);
            //}
            //else //attack 범위 안에있을때
            //{
            //    if(_enemy.enemyType == EnemyType.OneShotFlying)
            //    {
            //        Debug.Log("OneShotFlying Check = true");
            //        hpManager.HP = 0;// hp를 0으로 만들어서 터지게
            //    }
            //    else
            //    {
            //        if (_isCanAttack) //공격중이 아닐때 / 공격할수 있는지 감지시작한다
            //        {
            //            _isChasing = false;
            //            speed = 0;
            //            AttackPlayer();
            //        }
            //        else
            //        {
            //            speed = 0;
            //        }
            //    }
            //}
            //}
            //else
            //{
            //    _isChasing = false;
            //    speed = _enemy.BeforeDetectSpeed();
            // 
            //}
            #endregion
        }
        //else isCanAttack = false;
    }

    private void AttackPlayer()
    {
        anim.Attack();
    }

    public IEnumerator AttackCount()
    {
        endAttack = false;
        isWaitingAttackCool = true;
        yield return new WaitForSeconds(_enemy.AttackDelay());
        isCanAttack = true;
        isWaitingAttackCool = false;
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
