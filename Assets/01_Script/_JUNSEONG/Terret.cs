using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terret : EnemyBase
{
    private float attackDelay;
    private float detectRange;
    
    public float reloadCoolTime;
    public float chargeTime = 5f;
        
    public bool canDetect = true;
    public bool canAttack = false;
    public bool canMinusCool = true;
    public bool _isDetecting = false;
    public bool _isCharging = false;
    public GameObject Arrow;

    Vector2 Direction;

    //public GameObject Lazor;
    Lazor lazor;

    protected override void Awake()
    {
        base.Awake();
        attackDelay = _enemy.AttackDelay();
        reloadCoolTime = 0;
        detectRange = _enemy.DetectRange();
        lazor = transform.parent.GetComponentInChildren<Lazor>();
    }
   
    private void Update()
    {
        if(reloadCoolTime <= 0) reloadCoolTime = 0;
        else if(canMinusCool) reloadCoolTime -= Time.deltaTime;

        if (reloadCoolTime == 0)
        {
            canMinusCool = false;
            canAttack = true;
        }

        if (_isCharging) lazor.EnableLaser();
        else lazor.DisAbleLaser(); 

        if (Vector2.Distance(_target.position, transform.position) < detectRange)// || canDetect == true
        {
            TerretDetectPlayer();
        }
        else
        {
            _isCharging=false;
            _isDetecting = false;
            StopCoroutine("WaitAttackAndShoot");
            canAttack = true;
        }
    }

    Vector2 distanceFromPlayer;
    private void TerretDetectPlayer()
    {
        distanceFromPlayer = _target.position - this.gameObject.transform.position;
        RaycastHit2D detectPlayer = Physics2D.Raycast(transform.position, distanceFromPlayer);
        Debug.DrawRay(transform.position, distanceFromPlayer, Color.red);
        if (detectPlayer)
        {
            _isDetecting = true;
            if (_isCharging)
            {
                float angle = Mathf.Atan2(distanceFromPlayer.y, distanceFromPlayer.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                lazor.UpdateLaser(transform.position, detectPlayer);
            }
            if (detectPlayer.collider.CompareTag("Player") && canAttack)
            {
                canAttack = false;
                reloadCoolTime = attackDelay;
                StopCoroutine("WaitAttackAndShoot");
                StartCoroutine("WaitAttackAndShoot");
            }
        }
        else
        {
            _isDetecting = false;
        }
        
    }

    private Quaternion shootingAngle;
    private Vector2 dir2;
    public int arrowSpeed = 100;

    IEnumerator WaitAttackAndShoot()
    {
        _isCharging = true;
        yield return new WaitForSeconds(chargeTime);
        
        lazor.LazerFade();
        yield return new WaitForSeconds(0.7f);
        shootingAngle = transform.rotation;
        _isDetecting = false;
        dir2 = distanceFromPlayer;
        _isCharging = false;
        yield return new WaitForSeconds(0.5f);
        
        ShootArrow(shootingAngle, dir2);
        Debug.Log("endAttack");
        canMinusCool = true;
    }

    void ShootArrow(Quaternion angle , Vector2 dir)
    {
        GameObject arrow = Instantiate(Arrow, transform.position, angle);
        arrow.GetComponent<Rigidbody2D>().AddForce(dir * arrowSpeed, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
