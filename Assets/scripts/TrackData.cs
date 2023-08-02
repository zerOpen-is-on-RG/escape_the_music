using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrackData : MonoBehaviour
{
    public Tilemap structure;
    public AudioClip music;
    public TrackEffectBase effect;

    [HideInInspector]
    public bool isAwaked = false;

    private void Start()
    {
        isAwaked = true;
    }
}
