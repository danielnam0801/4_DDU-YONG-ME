using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class Attack : MonoBehaviour
{
    public Queue<Weapon> weapons;
    public int Spear_Count;
    public GameObject Spear;
    public GameObject  _weapon;
    [SerializeField]GameObject _grabPoint;
    public Weapon _weaponsc;
    public float Power;
    [SerializeField] float angle;
    [SerializeField] Vector2 dir;
    private void Awake()
    {
        weapons = new Queue<Weapon> ();
    }
    void Update()
    {
        if (_weapon)
        {
            _weapon.transform.position = _grabPoint.transform.position;
            dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _grabPoint.transform.position;
            angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;
            _weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            if (Input.GetMouseButtonDown(0))
                {
                _weaponsc.Shooting(dir, Power);
                _weaponsc = null;
                _weapon = null;
            }
        }else if (weapons.Count >0) 
        {
            Debug.Log(weapons.Peek().name);
            _weaponsc = weapons.Dequeue();
            _weaponsc.gameObject.SetActive(true);
            _weapon = _weaponsc.gameObject;
        }
        else
        {
            _weapon = null;
            _weaponsc = null;

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Weapon>() )
        {
            Weapon script = (collision.gameObject.GetComponent<Weapon>());
            if(script.state == State.Item)
            {
                weapons.Enqueue(script);
                script.Grab();
                script.gameObject.SetActive(false);

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
