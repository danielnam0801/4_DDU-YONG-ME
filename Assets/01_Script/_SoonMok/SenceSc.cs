using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenceSc : MonoBehaviour
{
    public enum Item
    {
        Ambush,
        FakePlat,
        Running,
        Shooting
    }
    public Item item;
    public delegate void SenceEvent();
    public SenceEvent Dosomething;
    [SerializeField] float speed;
    [SerializeField] GameObject arrow;
    [SerializeField] int idx = 0;
    [SerializeField] private Vector3[] points;
    [SerializeField] bool isActive;
    public void SetDoSomeThing(SenceEvent ii)
    {
        Dosomething = ii;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerHP>())Dosomething?.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>()) Dosomething?.Invoke();
    }
    private void Awake()
    {
        if (item == Item.FakePlat)
        {

            SetDoSomeThing(() =>
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
            });
        }

        for (int i = 0; i < points.Length; i++)
        {
            points[i] += transform.position;
        }
        if(item == Item.Shooting)
        {
            Dosomething += () =>
            {
                if (isActive)
                {
                    GameObject Arrow = Instantiate(arrow);
                    Arrow.transform.position = transform.position;
                    Arrow.GetComponent<Arrow>().dir = GameObject.Find("Player").transform.position - transform.position;
                    StartCoroutine(SetAcitve());
                }
            };
            
        }
    }
    IEnumerator SetAcitve()
    {
        isActive = false;
        yield return new WaitForSeconds(1);
        isActive = true;
    }
    private void Update()
    {
        if(item == Item.Running)
        {
            transform.position += (points[idx] - transform.position).normalized * Time.deltaTime * speed;
            if(Vector3.Distance(transform.position, points[idx]) < 0.5f)
            {
                idx++;
            }
            if (idx >= points.Length) idx = 0;
        }
    }
}
