using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private bool _isActing;
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    GameObject Player;
    
    public enum State
    {
        Chase,
        Attack1,
        Attack2,
        Defense
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        StartCoroutine(Act());
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator Act()
    {
        while (true)
        {

            if (!_isActing)
            {
                if (transform.position.y < Player.transform.position.y)
                {
                    _rigidbody.AddForce(Vector2.up * 5f);
                    yield return new WaitForSeconds(1f);
                }
                 if (Mathf.Abs(transform.position.x - Player.transform.position.x) > 0.7f)
                {
                    Vector3 dir = Player.transform.position - transform.position;
                    Debug.Log(dir);
                    transform.position += dir.normalized * _speed * Time.deltaTime;
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
        }

    }
}
