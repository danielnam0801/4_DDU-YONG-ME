using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private string playerLayerName;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(playerLayerName))
        {
            GetKeyObject getKeyScript = col.gameObject.GetComponent<GetKeyObject>();
            getKeyScript.gettingKey = true;
            Destroy(gameObject);
        }
    }

}
