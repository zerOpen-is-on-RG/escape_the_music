using System;
using UnityEngine;

public class TrackEffectBase : MonoBehaviour
{
    public Action<float> EffectUpdate;

    public GameManager gameManager;
    void Awake()
    {
        EffectUpdate = _update;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void _update(float timeline)
    {

    }
}
