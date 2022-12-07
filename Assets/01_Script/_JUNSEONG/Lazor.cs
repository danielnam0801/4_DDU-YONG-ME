using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazor : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public LineRenderer lineRenderer;
    Transform terret;

    private void Awake()
    {
        lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //lineRenderer = GetComponent<LineRenderer>();
        terret = gameObject.transform.parent.GetComponent<Transform>();
    }

    public void EnableLaser()
    {
        lineRenderer.enabled = true;
        //StartCoroutine("LazorShot");
    }

    public void UpdateLaser(Vector2 transform,RaycastHit2D hit)
    {
        lineRenderer.SetPosition(0, transform);
        lineRenderer.SetPosition(1, (hit.point));
    }

    public void DisAbleLaser()
    {
        lineRenderer.enabled = false;
    }

    public void LazerFade()
    {
        StartCoroutine("LazorShot");
    }

    public void LazerSpeed()
    {
        StartCoroutine("LazorSpeed");
    }

    IEnumerator LazorSpeed() // lazor 안에 물질 움직이는 속도 늘리는 디테일 추가하려면 여기서 구현
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator LazorShot()
    {
        
        for(int i = 0; i < 10; i++)
        {
            yield return StartCoroutine(FadeEffect(1,0));
            yield return StartCoroutine(FadeEffect(0,1));
        }
    }

    IEnumerator FadeEffect(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / 0.3f;
            Color color = lineRenderer.material.color;
            //color.b = Mathf.Lerp(start, end, percent);
            //color.g = Mathf.Lerp(start, end, percent);
            lineRenderer.material.color = color;
            
            yield return null;
        }
    }
}
