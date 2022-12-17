using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : EnemyBase
{
    FlyingEnemy flyingEnemy;
    GameObject bullet;

    private void OnEnable()
    {
        flyingEnemy = transform.parent.GetComponent<FlyingEnemy>();
    }

    public void Shoot()
    {
        Vector2 dir = _target.position - transform.position;
        GameObject Bu = Instantiate(bullet, transform.position, Quaternion.Euler(dir));
    }
}
