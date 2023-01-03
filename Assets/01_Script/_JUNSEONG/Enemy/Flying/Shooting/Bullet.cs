using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    Animator animator;
    [SerializeField] float Sizeduration = 1.5f;
    [SerializeField] float sizeValue = 2f;
    [SerializeField] Ease ease;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        transform.DOScale(transform.localScale * sizeValue, Sizeduration).SetEase(ease); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //플레이어의 hp --
            Dead();
        }
        else if(collision.gameObject.layer != 10)// EnemyLayer가 아닌경우만
        {
            Dead();    
        }
    }

    private void Dead()
    {
        animator.SetTrigger("isDead");
        Destroy(GetComponent<Rigidbody2D>());
    }

    public void DeadInit()
    {
        Destroy(this.gameObject);
    }
}
