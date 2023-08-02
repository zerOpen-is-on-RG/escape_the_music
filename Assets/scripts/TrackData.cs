using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrackData : MonoBehaviour
{
    public Tilemap structure;
    public string music;
    public TrackEffectBase effect;

    [HideInInspector]
    public TrackPattern pattern;

    [HideInInspector]
    public bool isAwaked = false;

    private void Start()
    {
        pattern = GetComponent<TrackPattern>();
        isAwaked = true;
    }

    public float MathperfectTime(Pattern _pattern)
    {
        return Mathf.Floor((_pattern.timeline - 1) * 10) / 10;
    }
}
