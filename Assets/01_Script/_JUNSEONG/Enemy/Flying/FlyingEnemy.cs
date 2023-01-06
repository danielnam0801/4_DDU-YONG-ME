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

    [Header("Enemy����")]
    public bool isCanChase = false;
    public bool isAttacking = false;
    public bool startAttack = false;
    public bool endAttack = false;
    public bool isCanAttack = true; // ���� ��Ÿ���� ������������ üũ
    public bool isWaitingAttackCool = false;
    public bool isMoving = false;

    bool OneShotEnemyDetectPlayer = false;

    float distanceFromPlayer; // �÷��̾�� �� ���� �Ÿ�

    FlyingEnemyAnim anim;
    EnemyHPManager hpManager;

    public LayerMask HideVision;
    private void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
        lineOfSite = _enemy.DetectRange();
        attackLineOfSite = _enemy.AttackRange();
        anim = transform.GetChild(0).GetComponent<FlyingEnemyAnim>();// ����� ��������Ʈ�� �� ��ũ��Ʈ�� �ִ� �� �ٷ� �ؿ� �����ؾ���
        hpManager = transform.GetComponent<EnemyHPManager>();
        groundLayer = Define.Floor;
        //if(_enemy.enemyType == EnemyType.FlyingAttack)
        //{
            
        //}
        //speed = _enemy.BeforeDetectSpeed();
    }

    private void Update()
    {
        distanceFromPlayer = Vector2.Distance(_target.transform.position, transform.position); // target�� �� ���� �Ÿ�

        if (_enemy.enemyType == EnemyType.OneShotFlying)// ��� ���� �ѹ��� ����
        {
            if (isAttacking)
            {
                hpManager.HP = 0; // ������ ���۵Ǹ� ���� �ȿ� ���Դٴ� �������� ü���� 0���� ���� ��Ʈ�� 
            }
        }

        if((_enemy.enemyType == EnemyType.FlyingAttack || _enemy.enemyType == EnemyType.OneShotFlying) && !isAttacking) //�������� �ƴϰ� 
        {
            Debug.Log("OneShot FLying  " + gameObject.name);
            DetectPlayer();
        }

        anim.IsChasing(isCanChase && !isAttacking); // ������ �����ϰ� �������� �ƴѰ�� true��ȯ
        anim.IsIdle(!isCanChase && !isAttacking); //�������� �ƴϰų� �������� �ƴҰ�� true��ȯ
        
        HitDetection(); // ������ �浹 ����
      
        if (endAttack) StartCoroutine(AttackCount());
        #region ���ݰ���
        //if (isAttacking) speed = 0; // �������϶��� �����
        if (!isCanChase) // �÷��̾�� ��(��) ���̿� �����ְų� ���� ���� ����
        {
            speed = _enemy.BeforeDetectSpeed();
        }
        else if(!isAttacking && isCanAttack)//���� �ȿ� �ְ� �÷��̾�� �� ���̿� �ƹ��͵� ������� 
        {
            if (CanAttackCheck())// ������ �������� üũ
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
        return (distanceFromPlayer < attackLineOfSite); // target�� Attack �����Ÿ� �ȿ��ְ� ������ ������ �����϶� true�� ��ȯ
    }

    #region ��������Ʈ ������
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


    private void DetectPlayer() // �÷��̾� ����
    {
        if (distanceFromPlayer < lineOfSite)
        {
            int layerMask = (-1) - (1 << LayerMask.NameToLayer("Enemy"));
            RaycastHit2D canChasePlayer = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, 15 ,HideVision);; // EnemyLayer�� ������ ��� ���̾ ����
            Debug.Log("���� ���� ���� ������Ʈ : ", canChasePlayer.collider);
            Debug.DrawRay(transform.position * 5, _target.position - transform.position);
            if (canChasePlayer.collider.CompareTag("Player"))// ���� ���� ���� ���̰� �÷��̾��϶� // ������ �� �ְ� ��
            {
                Debug.Log("��������!");
                isCanChase = true; // ����� ��ֹ��� ���̿� ������ ������ �׳� ����� ���
            }
            else isCanChase = false;
            
            if(_enemy.enemyType == EnemyType.OneShotFlying) // Trigger Ȱ��ȭ �ѹ��� �ǰ� �ϴ� �ڵ�
            {
                if (OneShotEnemyDetectPlayer == false)
                {
                    anim.DetectPlayer();
                    OneShotEnemyDetectPlayer = true;
                }
            }
            #region �ٲٱ� ��
            //if(_enemy.enemyType == EnemyType.OneShotFlying)
            //PlayerPosCheck();
            //if (distanceFromPlayer > attackLineOfSite && !_isAttacking) // attack���� �ٱ��ʿ� ������
            //{
            //    _isChasing = true;
            //    speed = _enemy.AfterDetectSpeed();
            //    Debug.Log("�Ѿư�����");
            //    transform.position = Vector2.MoveTowards(this.transform.position, _target.position, speed * Time.deltaTime);
            //}
            //else //attack ���� �ȿ�������
            //{
            //    if(_enemy.enemyType == EnemyType.OneShotFlying)
            //    {
            //        Debug.Log("OneShotFlying Check = true");
            //        hpManager.HP = 0;// hp�� 0���� ���� ������
            //    }
            //    else
            //    {
            //        if (_isCanAttack) //�������� �ƴҶ� / �����Ҽ� �ִ��� ���������Ѵ�
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
