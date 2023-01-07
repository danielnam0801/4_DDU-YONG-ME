using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : EnemyBase
{
    FlyingEnemy flyingEnemy;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootingPower = 3f;

    private void Start()
    {
        flyingEnemy = GetComponent<FlyingEnemy>();
        shootPoint = transform.Find("ShootPoint").GetComponent<Transform>();
    }

    public void Shoot()
    {
        Vector2 dir = _target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject Bu = Instantiate(enemyBullet, shootPoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Bu.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shootingPower,ForceMode2D.Impulse);
    }
    

}
