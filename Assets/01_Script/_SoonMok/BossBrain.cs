using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : MonoBehaviour
{
    [SerializeField] private float _firstSpeed;
    [SerializeField] private float _secondSpeed;
    [SerializeField] GameObject LHand;
    [SerializeField] GameObject RHand;
    [ContextMenu("StartPatton")]
    public void StartPatton()
    {
        StartCoroutine(Pattons());
    }
    IEnumerator Pattons()
    {
        while (true)
        {
            yield return new WaitForSeconds(_firstSpeed);
            switch(Random.Range(0, 2))
            {
                case 0:
                    Instantiate(LHand);
                    break;
                case 1:
                    break;
            }
        }
    }
}
