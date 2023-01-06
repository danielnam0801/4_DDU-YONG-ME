using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spearcount : MonoBehaviour
{
    [SerializeField] Attack _attackSc;
    private int bulletCount = 0;
    private int bulletTextCount = 3;

    [SerializeField] TMPro.TextMeshProUGUI bulletText;

    void Start()
    {
        _attackSc = FindObjectOfType<Attack>();
    }

    void Update()
    {
        
        bulletText.text = $"{_attackSc.weapons.Count}/{_attackSc.Spear_Count}";
    }
    
}
