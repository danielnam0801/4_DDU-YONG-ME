using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamInit : MonoBehaviour
{

    CinemachineVirtualCamera cam;
    private void Awake()
    {
        cam = Define.VCam;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
