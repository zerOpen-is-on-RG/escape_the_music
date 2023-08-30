using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class MinorEffect : TrackEffectBase
{
    public GameObject background;
    public Tilemap map;
    void Start()
    {
        EffectUpdate = update_;

        map.transform.localPosition = new Vector2(0, 0);
    }

    void update_(float timeline)
    {
    }
}
