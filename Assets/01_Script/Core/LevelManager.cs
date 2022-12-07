using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<int> levels = new List<int>();
    [SerializeField] 
    public int currentLevel = 0;
    public int totalExp = 0;

    private void Update()
    {
     
    }
}
