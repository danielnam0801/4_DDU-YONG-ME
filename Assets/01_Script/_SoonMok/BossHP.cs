using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int MaxHp;
    public int Hp;
    [SerializeField] private bool _hitting;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField]float i = 0;
    private void OnEnable()
    {
        Hp = MaxHp;
    }
    [SerializeField] bool _isDead;
    [ContextMenu("´Ù¸¶°Ô")]
    public void Damage(int damage = 1)
    {
        if (!_hitting)
        {
            Hp -= damage;
            StartCoroutine(Apayo());

        }
    }    
    
    IEnumerator Apayo()
    {
        _hitting = true;
        for(int i = 0; i < 10; i++)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        _hitting = false;
    }
    private void Update()
    {
        if (Hp <= 0) _isDead = true;
        if (_isDead)
        {
            i += 180 * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0,0,i);
            transform.localScale -= new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, 0);
            if (transform.localScale.x < 0) Destroy(gameObject);
        }
    }
}
