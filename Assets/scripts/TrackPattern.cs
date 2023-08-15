using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPattern : MonoBehaviour
{
    public int patternSpeed = 20;

    public bool clearPattern = false;
    public List<Pattern> patterns;

    public bool startTracking = false;
    public bool stopTracking = false;
    public bool isTracking = false;
    bool _tracking = false;

    TrackData trackData;
    GameManager gameManager;

    float timeline = 0;

    private void Awake()
    {
        startTracking = false;
        stopTracking = false;
        _tracking = false;
        trackData = GetComponent<TrackData>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        isTracking = _tracking;
        if (trackData.isAwaked && startTracking)
        {
            startTracking = false;

            StartCoroutine(_startTracking());
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

        if (_tracking)
        {
            timeline += Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                float pointTime = Mathf.Floor(timeline * 100) / 100;

                Pattern pattern = new Pattern();
                pattern.name = pointTime.ToString();
                pattern.timeline = pointTime;
                pattern.x = point.x;

                var particle = Instantiate(gameManager.trackingParticle, point, Quaternion.identity);
                Destroy(particle, 0.8f);

                patterns.Add(pattern);
            }
        }
    }

    IEnumerator _startTracking()
    {
        timeline = 0;
        for (int i = 3; i > 0; i--)
        {
            gameManager.title.text = i.ToString();
            gameManager.title.color = Color.blue;
            yield return new WaitForSeconds(1);
        }

        gameManager.title.text = "";
        gameManager.soundManager.Play(trackData.music);
        _tracking = true;
    }
}
