using System;
using UnityEngine;

public class TrackEffectBase : MonoBehaviour
{
    public Action<float> EffectUpdate;
    void Awake()
    {
        EffectUpdate = _update;
    }

    void _update(float timeline)
    {

    }
}
