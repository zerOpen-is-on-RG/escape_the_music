using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Vcam : MonoBehaviour
{
    public CinemachineVirtualCamera vir;
    public CinemachineBasicMultiChannelPerlin noise;
    void Start()
    {
        vir = GetComponent<CinemachineVirtualCamera>();
        noise = vir.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float strength = 1, float dur = 0.05f)
    {
        StartCoroutine(_shake(strength, dur));
    }

    IEnumerator _shake(float strength, float dur)
    {
        noise.m_AmplitudeGain = strength;

        yield return new WaitForSeconds(dur);

        noise.m_AmplitudeGain = 0;
    }
}
