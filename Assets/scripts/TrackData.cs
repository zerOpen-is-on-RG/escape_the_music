using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrackData : MonoBehaviour
{
    public Tilemap structure;

    public string _name;
    public string displayName;
    public string author;
    public Sprite logoBackground;

    public string music;
    public int trackTime;
    public TrackEffectBase effect;

    [HideInInspector]
    public TrackPattern pattern;

    public bool isAwaked = false;

    private void Start()
    {
        pattern = GetComponent<TrackPattern>();
        effect = GetComponent<TrackEffectBase>();
        isAwaked = true;
    }

    public float MathperfectTime(Pattern _pattern)
    {
        return Mathf.Floor((_pattern.timeline - 2 + 0.05f * pattern.patternSpeed) * 10) / 10;
    }
}
