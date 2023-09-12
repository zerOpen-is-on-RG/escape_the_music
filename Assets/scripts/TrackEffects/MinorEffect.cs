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
    public GameObject background2;
    public Tilemap map;
    public GameObject[] obstacles;

    float backMotionTimer = 0;
    bool backMotion = false;
    string state = "";
    string motioning = "";

    private List<GameObject> objects = new();
    public List<float> spawnedObjects = new();
    public Transform effect1;


    void Start()
    {
        EffectUpdate = update_;

        map.transform.localPosition = new Vector2(0, 0);
        EffectUpdate = update_;
        background.SetActive(false);
        map.gameObject.SetActive(false);
        gameManager.effect1.SetActive(true);
        gameManager.detecting = true;

    }

    void update_(float timeline)
    {
        backMotionTimer+=Time.deltaTime;
        var col=map.color;
        if(timeline<=2.7f)
        {
            gameManager.vcam.Shake(2);
        }
        if(timeline>=2.7f&&state!="2.7B"&&timeline<=2.9f)
        {
            state="2.7B";
            map.gameObject.SetActive(true);
            gameManager.effect1.SetActive(false);
        }
        if(timeline>=2.7f&&timeline<16.9f)
        {
            gameManager.vcam.Shake(1);
        }
        if (timeline >= 16.85f && state !="16.85B" &&timeline<=17.0)
        {
            background.SetActive(true);
            state = "16.85B";
            gameManager.detecting = false;
            map.transform.DOLocalMove(new Vector2(25, 0), 1.5f);
        }
        if(timeline>=16.85f&&timeline<=40.5f)
        {
            gameManager.detecting = true;
            gameManager.vcam.Shake(2);
        }
        if(timeline>=43.5&&state!="43.9"&&timeline<=44.1)
        {
            state = "43.9";
            map.transform.localPosition = new Vector2(45, -10);
            gameManager.vcam.Shake(10);
        }
        if(timeline>=44.3&&state!="44.3B"&&timeline<=44.5)
        {
            gameManager.detecting = false;
            state = "44.3B";
            map.transform.localPosition = new Vector2(45, 0);
            gameManager.vcam.Shake(10);
            foreach (Transform child in background.transform)
            {
                child.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        if (timeline >= 44.8 && state != "44.5B" && timeline <= 44.9)
        {
            state = "44.5B";
            map.transform.localPosition = new Vector2(68, 0);
            gameManager.vcam.Shake(10);
            foreach (Transform child in background.transform)
            {
                child.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        if (timeline >= 45.3 && state != "45.3B" && timeline <= 45.4)
        {
            state = "45.3B";
            map.transform.localPosition = new Vector2(88, 0);
            gameManager.vcam.Shake(10);
            foreach (Transform child in background.transform)
            {
                child.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
        if(timeline>=45.4&&timeline<49.0)
        {
            gameManager.detecting = true;
            gameManager.vcam.Shake(1);
        }
        if(timeline>=49.5&&state!="49.5B"&&timeline<=49.6)
        {
            state = "49.5B";
            gameManager.vcam.Shake(8);
        }
        if (timeline >= 50.0 && state != "50.0B" && timeline <= 50.1)
        {
            state = "50.0B";
            gameManager.vcam.Shake(8);
        }
        if (timeline >= 50.5 && state != "50.5B" && timeline <= 50.7)
        {
            state = "50.6B";
            gameManager.vcam.Shake(8);
        }
        if (timeline >= 50.7 && timeline < 82)
        {
            gameManager.vcam.Shake(1);
        }
        if (timeline>=80.3&&state!="80.3B"&&timeline<=80.4)
        {
            gameManager.detecting = false;
            map.transform.DOLocalMove(new Vector2(105, 0), 1.5f);
            background.SetActive(false);
        }

        if (timeline >= 82 && timeline < 103.4)
        {
            gameManager.detecting = true;
            gameManager.vcam.Shake(1);
        }
        if (timeline>=103.4&&state!="103.B"&&timeline<=104)
        {
            gameManager.detecting = false;
            state = "103.B";
            map.transform.DOLocalMove(new Vector2(135, 0), 2.5f);
        }
    }
}
