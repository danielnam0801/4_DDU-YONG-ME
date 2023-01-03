using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHitable
{
    public bool IsEnemy { get;  }
    public Vector3 HitPoint { get; set; }
    public void GetHit(float damage, GameObject damageDealer);

}
