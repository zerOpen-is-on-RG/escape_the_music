using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BlueBirdEffect : TrackEffectBase
{
    public GameObject background;
    public Tilemap map;

    float backMotionTimer = 0;
    bool backMotion = false;
    string state = "";
    string motioning = "";
    void Start()
    {
        EffectUpdate = update_;
        background.SetActive(false);
        map.gameObject.SetActive(false);

        map.transform.localPosition = new Vector2(0, 0);
    }

    void update_(float timeline)
    {
        backMotionTimer += Time.deltaTime;

        var col = map.color;

        if (timeline <= 0.3f) map.gameObject.SetActive(true);

        if (timeline <= 1.5f)
        {
            col.a = timeline / 1.5f;

            if (timeline >= 1) gameManager.detecting = true;
        }

        if (timeline > 11f && timeline < 13f)
        {
            col.a = 1 - ((timeline - 11) / 2f);

            if (timeline > 12) gameManager.detecting = false;

            if (timeline > 11.5f && !motioning.Equals("11.5P"))
            {
                motioning = "11.5P";
                gameManager.player.transform.DOLocalMove(gameManager.player.default_pos, 2);
            }
        }

        if (timeline >= 11)

        if (timeline >= 14f && timeline <= 14.5f) gameManager.blur.SetActive(true);
        if (timeline >= 14.5f && timeline <= 14.8f)
            {
            map.transform.localPosition = new Vector2(20, 0);
            col.a = 1;
            gameManager.blur.SetActive(false);
            background.SetActive(true);

            gameManager.vcam.Shake(2);

            gameManager.detecting = true;

            backMotion = true;
        }

        if (backMotion)
        {
            if (!motioning.Equals("A") && backMotionTimer <= 0.3f)
            {
                StartCoroutine(motionA());
                motioning = "A";
            }
            else if (backMotionTimer >= 6f)
            {
                motioning = "";
                backMotionTimer = 0;
            }
        }

        if (timeline >= 30f && timeline <= 30.2f)
        {
            gameManager.detecting = false;
            map.transform.localPosition = new Vector2(40, map.transform.localPosition.y);
            gameManager.vcam.Shake(2);
        }
        if (timeline >= 30.4f && !state.Equals("26.12T") && timeline <= 30.7f)
            {
            state = "26.12T";
            backMotion = false;

            map.transform.DOLocalMove(new Vector2(40, 0), 1.5f);

            background.transform.DOKill();
            background.transform.DOLocalRotate(new Vector3(0, 0, 0), 2f);
            background.transform.DOScale(new Vector3(1, 1), 2f);
        }

        if (timeline >= 33.1f && !state.Equals("33.1T") && timeline <= 34f)
        {
            state = "33.1T";
            backMotionTimer = 0.5f;
        }

        if (state.Equals("33.1T") && backMotionTimer >= 0.3f && timeline <= 34f)
        {
            map.transform.localPosition = new Vector2(map.transform.localPosition.x + 2f, map.transform.localPosition.y);

            gameManager.detecting = true;

            gameManager.vcam.Shake(1);
            backMotionTimer = 0;
        }
        if (state.Equals("33.1T") && backMotionTimer >= 0.1f && timeline > 34f)
        {
            map.transform.localPosition = new Vector2(map.transform.localPosition.x + 0.5f, map.transform.localPosition.y);
            backMotionTimer = 0;
        }

        if (timeline >= 35.3f && !state.Equals("35.3B"))
        {
            map.transform.localPosition = new Vector2(60, 0);
            state = "35.3B";
            foreach (Transform child in background.transform)
            {
                child.GetComponent<SpriteRenderer>().color = new Color(0, 192, 255, 255);
            }
            gameManager.vcam.noise.m_AmplitudeGain = 1;
        }

        map.color = col;
    }
    IEnumerator motionA()
    {
        background.transform.DOLocalRotate(new Vector3(0, 0, 31), 3f);
        background.transform.DOScale(new Vector3(0.9f, 0.9f), 3f);

        yield return new WaitForSeconds(3f);

        background.transform.DOLocalRotate(new Vector3(0, 0, -24), 3f);
        background.transform.DOScale(new Vector3(1.5f, 1.5f), 3f);

        yield return new WaitForSeconds(3f);
    }
}
