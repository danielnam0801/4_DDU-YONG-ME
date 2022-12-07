using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private List<ItemSO> _dropTable;
    private float[] _itemWeights;

    [SerializeField] private float _dropPower = 2f;
    [SerializeField]
    [Range(0f, 1f)]
    private float _dropChance;

    private void Start()
    {
        _itemWeights = _dropTable.Select(item => item.rate).ToArray();
    }

    public void Drop()
    {
        float dropVariable = Random.value;
        if(dropVariable < _dropChance)
        {
            int index = GetRandomItemIndex();
            //
        }
    }

    private int GetRandomItemIndex()
    {
        float sum = 0f;
        for(int i = 0; i < _itemWeights.Length; i++)
        {
            sum += _itemWeights[i]; 
        }
        float randomValue = Random.Range(0,sum);
        float tempSum = 0f;

        for(int i = 0; i< _itemWeights.Length; i++)
        {
            if(randomValue >= tempSum && randomValue < tempSum + _itemWeights[i])
            {
                return i;
            }
            else
            {
                tempSum += _itemWeights[i];
            }
        }
        return 0;
    }
}
