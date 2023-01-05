using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossHand : MonoBehaviour
{
    public Vector3[] Direction;
    public int Pattons;
    public GameObject PlayerObject;
    public bool Waitting;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField]CinemachineBasicMultiChannelPerlin a;
    [SerializeField] GameObject _stone;
    [SerializeField] private float _horizontalDistance;
    [SerializeField] private float _verticalDistance;
    [SerializeField] private Vector3 _startPos;
    public bool _isEnd;
    // Start is called before the first frame update
    void Start()
    {
    }
    
    public void SetPatton(int num, float wait)
    {
        Pattons = num;
        a = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        transform.localPosition = _startPos;
        StopAllCoroutines();
        Waitting = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Pattons == 0)
        {
            
            if (Waitting)
            {
                transform.position = PlayerObject.transform.position + (Vector3.up * 5);
                StartCoroutine(Delay(1f));
            }
            else
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                StartCoroutine(UnDelay(1.5f));
            }   
        }
        if(Pattons == 1)
        {
            if (Waitting)
            {
                if (!Physics2D.Raycast(transform.position, Vector2.down, 0.3f, _layerMask))
                {
                    transform.position += Vector3.down * speed * Time.deltaTime;
                    cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                    cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
                }
                else Waitting = false;
            }
            else
            {
                cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 4;
                cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 2;
                StartCoroutine(ShakeCave());
                Pattons = 3;
            }
        }
        if(Pattons == 3)
        {
            transform.Translate((_startPos - transform.localPosition)* Time.deltaTime);
        }
    }
    IEnumerator Delay(float a)
    {
        Waitting = true;
        yield return new WaitForSeconds(a);
        Waitting = false;
    }IEnumerator UnDelay(float a)
    {
        Waitting = false;
        yield return new WaitForSeconds(a);
        Waitting = true;
    }
    IEnumerator ShakeCave()
    {
        for (int i = 0; i < 5; i++) {
            GameObject a = Instantiate(_stone);
            a.transform.position = transform.position + new Vector3(i * _horizontalDistance, _verticalDistance);
            yield return new WaitForSeconds(0.4f);
        }
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHP>())
        {
            collision.gameObject.GetComponent<PlayerHP>().Damage(1);


        }

    }
}
