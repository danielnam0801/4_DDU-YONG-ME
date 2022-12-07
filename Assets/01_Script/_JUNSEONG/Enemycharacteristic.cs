using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemycharacteristic : MonoBehaviour
{
    [SerializeField] EnemySO _enemy;
    private void Awake()
    {
        switch (_enemy.enemyType)
        {
            case EnemyType.NormalEnemy:
                break;
            case EnemyType.ShieldEnemy:
                //쉴드만의 특성을 주는 함수 호출
                break;
            case EnemyType.Boss:
                //보스만의 특성을 주는 함수 호출
                break;
            default:
                Debug.Log("존재하지 않는 적 타입이야!");
                break;
        }
    }

    void ShieldEnemy()
    {

    }

    void Boss()
    {

    }
}
