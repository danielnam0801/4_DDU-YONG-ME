using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : EnemyBase
{
    FlyingEnemy flyingEnemy;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] float shootingPower = 3f;

    private void OnEnable()
    {
        flyingEnemy = transform.parent.GetComponent<FlyingEnemy>();
    }

    public void Shoot()
    {
        Vector2 dir = _target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject Bu = Instantiate(enemyBullet, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        Bu.GetComponent<Rigidbody2D>().AddForce(dir.normalized * shootingPower,ForceMode2D.Impulse);
    }
    

}
