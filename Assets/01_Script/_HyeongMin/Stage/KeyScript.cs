using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private string playerLayerName;

    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = transform.Find("Audio").GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer(playerLayerName))
        {
            _audioSource.Play();
            GetKeyObject getKeyScript = col.gameObject.GetComponent<GetKeyObject>();
            getKeyScript.gettingKey++;
            StartCoroutine("WaitSound");
        }
    }

    IEnumerator WaitSound()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}
