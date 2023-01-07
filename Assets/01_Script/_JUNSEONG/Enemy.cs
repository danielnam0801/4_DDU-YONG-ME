using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : EnemyBase
{
    int hp;
    public bool isEnemyDie = false;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        hp = _enemy.HP();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !isEnemyDie)
        {
            EnemyDead();
            isEnemyDie = true;
        }
    }

    public void EnemyDead()
    {
        DeadProcess();
    }

    public void DeadProcess()
    {
        levelManager.totalExp += _enemy.DropExPValue();
        //그이외의 죽었을때 실행시켜줄것
    }
}
