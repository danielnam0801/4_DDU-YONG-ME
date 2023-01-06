using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spearcount : MonoBehaviour
{
    public GameObject PoolBullet;
    public GameObject[] PoolBullets = new GameObject[3];
    private int bulletCount = 0;
    private int bulletTextCount = 3;

    [SerializeField] TMPro.TextMeshProUGUI bulletText;

    void Start()
    {

        for (int i = 0; i < PoolBullets.Length; i++)
        {
            PoolBullets[i] = Instantiate(PoolBullet);
            PoolBullets[i].SetActive(false);
        }
    }

    void Update()
    {
        bulletText.text = $"{bulletTextCount}/" + PoolBullets.Length.ToString();
    }

    public void bulletpool()
    {
        PoolBullets[bulletCount].SetActive(true);
        bulletCount++;
        bulletTextCount--;

    }
}
