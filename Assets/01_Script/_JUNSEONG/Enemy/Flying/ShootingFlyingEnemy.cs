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

    // ���� ���������� ������ �� ��ȭ��
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
                if (!isMoving) // �����̴� ���� �ƴϸ�
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
        RaycastHit2D canChasePlayer = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, 15, ~(Define.Weapon | Define.Enemy)); // EnemyLayer�� ������ ��� ���̾ ����
        Debug.Log("���� ���� ���� ������Ʈ : ", canChasePlayer.collider);
        Debug.DrawRay(transform.position * 5, _target.position - transform.position);
        if (canChasePlayer.collider != null)
        {
            if (canChasePlayer.collider.CompareTag("Player"))// ���� ���� ���� ���̰� �÷��̾��϶� // ������ �� �ְ� ��
            {
                Debug.Log("��������!");
                canDetectAttack = true; // ����� ��ֹ��� ���̿� ������ ������ �׳� ����� ���
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
    
    IEnumerator Moving(Vector2 InputTrans)// ȣ��� ������ ��ġ��
    {
        float randomXValue = UnityEngine.Random.Range(-0.5f, 0.5f);
        float randomYValue = UnityEngine.Random.Range(-0.02f, +0.02f);
        float RBeforeMovingTime = UnityEngine.Random.Range(-0.5f, 0.5f);
        float RAfterMovingTime = UnityEngine.Random.Range(-0.5f, 0.5f);
        
        float XSpeedValueControl = this.XSpeedValueControl + randomXValue;
        float YSpeedValueControl = this.YSpeedValueControl + randomYValue;

        float BackMovingTime = this.BackMovingTime + RBeforeMovingTime;
        float AfterMovingTime = this.BackMovingTime + RAfterMovingTime;
        
        #region �÷��̾ �����ʿ� ������ �̵�
        if (transform.localScale.x == 1)
        { //�÷��̾ �����ʿ� ���� ��
            float x = 0; 
            while (x <= BackMovingTime)
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
                float x1 = XSpeedValueControl * (Mathf.Sqrt(x + speedRelativeControl) - Mathf.Sqrt(speedRelativeControl)); //�� 1�и������� ���� �����Լ�
                float y1 = Mathf.Pow(x, 2) * YSpeedValueControl; // �� �Լ��� ���Լ�
                transform.position = new Vector3( MovingAfterPos.x + x1, MovingAfterPos.y - y1, 0);
                x += Time.deltaTime;
                yield return null;
            }
            yield return null;

            Debug.Log("�̵���");
            isMoving = false;
        }
        #endregion
        #region �÷��̾ ���ʿ� ������ �̵�
        else
        {
            float x = 0;
            while (x <= BackMovingTime)
            {
                // 0, 0 �� �������� ��� 
                float x1 = -XSpeedValueControl * (-Mathf.Sqrt(x + speedRelativeControl) + Mathf.Sqrt(speedRelativeControl)); // �� 3�и������� ���� �����Լ�
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
                float x1 = -XSpeedValueControl * (Mathf.Sqrt(x + speedRelativeControl) - Mathf.Sqrt(speedRelativeControl)); //�� 1�и������� ���� �����Լ�
                float y1 = Mathf.Pow(x, 2) * YSpeedValueControl; // �� �Լ��� ���Լ�
                transform.position = new Vector3(MovingAfterPos.x + x1, MovingAfterPos.y - y1, 0);
                x += Time.deltaTime;
                yield return null;
            }
            yield return null;

            Debug.Log("�̵���");
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
