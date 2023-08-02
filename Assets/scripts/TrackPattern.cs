using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TrackPattern : MonoBehaviour
{
    public int patternSpeed;

    public bool clearPattern = false;
    public List<Pattern> patterns;

    public bool startTracking = false;
    public bool stopTracking = false;
    public bool isTracking = false;
    bool _tracking = false;

    TrackData trackData;

    private void Awake()
    {
        startTracking = false;
        stopTracking = false;
        _tracking = false;
        trackData = GetComponent<TrackData>();
    }

    void Update()
    {
        isTracking = _tracking;
        if (trackData.isAwaked && startTracking)
        {
            _tracking = true;
            startTracking = false;
        }

        if (trackData.isAwaked && stopTracking)
        {
            _tracking = false;
            stopTracking = false;
        }

        if (trackData.isAwaked && clearPattern)
        {
            patterns.Clear();
            clearPattern = false;
        }
    }
}
