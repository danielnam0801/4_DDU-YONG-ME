using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyBase
{
    public float speed;
    public float afterNoChasingTime = 0f;

    Vector2 dir;
    public int Dir => (int)dir.x;
    
    Vector2 origin;
    protected int playerPosDir;

    public LayerMask layerMask;
    //LayerMask collisionAfterChangeDirLayer;
    Rigidbody2D _rigid;

    public int nextMove = 0;
    public float randThinkTime = 5f;

    //enemyState
    public bool _isThinking;
    public bool _isChasing;
    public bool _isCanDetectAttacking;

    RaycastHit2D detectPlayer;

    EnemyAttack _enemyAttack;

    //[SerializeField]
    //Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        this.speed = _enemy.BeforeDetectSpeed();
        _rigid = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _enemyAttack = GetComponent<EnemyAttack>();

        nextMove = Random.Range(-1, 2);

        _isThinking = true;
        _isChasing = false;
        _isCanDetectAttacking = false;
    }

    private void Update()
    {
        Debug.Log($"현재 참조하는 애니메잍터 : {_animator.name}");
        Debug.Log(nextMove);
        if (!_enemyAttack._isAttacking)
        {
            _rigid.velocity = new Vector2(nextMove * speed, _rigid.velocity.y);
        }
        else
        {
            _animator.SetBool("moving", false);
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        }

        if (nextMove != 0 && !_enemyAttack._isAttacking)
        {
            _animator.SetBool("moving", true);
        }
        else if (nextMove == 0)
        {
            _animator.SetBool("moving", false);
        }

        dir = (transform.localScale.x) >= 0 ? Vector2.right : Vector2.left; // 고쳐야될수도
        origin = (Vector2)transform.position + (nextMove >= 0 ? Vector2.right : Vector2.left);
        playerPosDir = _target.position.x - transform.position.x < 0 ? -1 : 1;

        if (_isThinking && !_enemyAttack._isAttacking)
        {
            PlatformCheck();
            DetectPlayer();

            if (nextMove == 1 && (!_enemyAttack._isAttack))
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (nextMove == -1 && (!_enemyAttack._isAttack))
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (_isChasing)
            {
                FaceTarget();
                nextMove = (int)dir.x;
            }

            if (_isChasing && !_enemyAttack._isAttack)
            {
                speed = _enemy.AfterDetectSpeed();
                afterNoChasingTime = 0;
                _animator.SetBool("moving", true);
            }

            if (!_isChasing)
            {
                if (nextMove != 0)
                {
                    speed = _enemy.BeforeDetectSpeed();
                }
                //PlatformCheck();
                //_animator.SetBool("moving", false);
                afterNoChasingTime += Time.deltaTime;
                if (afterNoChasingTime > randThinkTime)
                {
                    afterNoChasingTime = 0;
                    EnemyThink();
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            nextMove = -nextMove;
            afterNoChasingTime = Random.Range(0f, 1f);//벽충돌후 적 생각쿨타임
        }
    }

    public void FaceTarget()
    {
        if (_target.position.x - transform.position.x < 0) // 타겟이 왼쪽
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void EnemyThink()
    {
        Debug.Log("ENemyTHinkOpen");
        nextMove = Random.Range(-1, 2);
        print(nextMove);
        if (nextMove != 0)
        {
            randThinkTime = Random.Range(3f, 5f);
        }
        else
        {
            randThinkTime = Random.Range(1f, 2f);
            afterNoChasingTime = 0;
            if (afterNoChasingTime < randThinkTime)
            {
                afterNoChasingTime += Time.deltaTime;
            }
            else if (afterNoChasingTime > randThinkTime)
            {
                EnemyThink();
            }
        }
    }

    void DetectPlayer() // 그냥 거리재는거로 바꿔야 될수도 살짝 부자연스러움
    {

        //detectPlayer = Physics2D.Raycast(origin, dir, _enemy.DetectRange(), layerMask);
        detectPlayer = BoxCast(transform.position, new Vector2(4f, 1.5f), 0, dir, _enemy.DetectRange(), layerMask);

        if (detectPlayer.collider == null)
        {
            _isChasing = false;
        }
        else if (detectPlayer.collider.gameObject.tag == "Player")
        {
            _isChasing = true;
        }
        Debug.DrawRay(origin, dir * _enemy.DetectRange());
    }

    void PlatformCheck()
    {
        Vector2 rayDir = Vector2.down;
        RaycastHit2D platformCheckHit = Physics2D.Raycast(origin, rayDir, 3);
        Debug.DrawRay(origin, rayDir, Color.red, 0.1f);

        Debug.Log(platformCheckHit.collider);
        if (platformCheckHit.collider == null)
        {
            Debug.Log("isNull");
            nextMove = -nextMove;
            //afterNoChasingTime = 2;
            //_isThinking = false;

            //StartCoroutine(EnemyNextThinkTime(3));

        }
    }
    #region BoxCast
    static public RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance, int mask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance, mask);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }
    #endregion
}
