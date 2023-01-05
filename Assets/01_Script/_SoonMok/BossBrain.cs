using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBrain : MonoBehaviour
{
    [SerializeField] private float _firstSpeed;
    [SerializeField] private float _secondSpeed;
    [SerializeField] GameObject LHand;
    [SerializeField] GameObject RHand;
    [SerializeField] public int HP;
    [SerializeField] private Vector3 _origin;
    [SerializeField] private float i = 0;
    [ContextMenu("StartPatton")]

    private void OnEnable()
    {
        _origin = transform.position;
    }
    public void Awake()
    {
        StopAllCoroutines();
        StartCoroutine(Pattons());
        _origin = transform.position;
    }
    IEnumerator Pattons()   
    {
        while (true)
        {
            yield return new WaitForSeconds(_firstSpeed);
            if (HP < 9)
            {
                int a = Random.Range(0, 2);
                LHand.GetComponent<BossHand>().SetPatton(a,_firstSpeed);
                RHand.GetComponent<BossHand>().SetPatton(a,_firstSpeed);

            }
            else
            {
                int a = Random.Range(0, 2);
                LHand.GetComponent<BossHand>().SetPatton(a, _secondSpeed);
                RHand.GetComponent<BossHand>().SetPatton(a, _secondSpeed);

            }
        }
    }
    private void Update()
    {
        i += Time.deltaTime * 2.5f;
        transform.position = _origin + (new Vector3(Mathf.Sin(i), Mathf.Cos(i)));
        if (i > 360) i = 0;
    }
}
