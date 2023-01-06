using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExp : MonoBehaviour
{

    PlayerEXP exp;
    // Start is called before the first frame update
    void Start()
    {
        exp = GameManager.instance.Target.gameObject.GetComponent<PlayerEXP>();
    }


    public void DieExpUp()
    {
        exp.exp += 3;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
