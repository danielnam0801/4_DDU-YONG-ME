using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingTrap : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 dir;
    
    // Update is called once per frame
    private void Awake()
    {
        GetComponentInChildren<SenceSc>().SetDoSomeThing(Ambush);
        
    }
    public void Ambush()
    {
        StartCoroutine(ambush());
    }
    IEnumerator ambush()
    {
        
        for(int i = 0; i < 50; i++)
        {

            transform.position += (Vector3)dir.normalized * Time.deltaTime * speed;
            yield return new WaitForSeconds(0.01f/6);

        }
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 50; i++)
        {

            transform.position += (Vector3)dir.normalized * Time.deltaTime * (-speed-0.177f);
            yield return new WaitForSeconds(0.01f/6);

        }
        transform.position += (Vector3)dir.normalized * Time.deltaTime * -speed;

    }
}
