using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public Player player;
    public Canvas noteLine;
    public Note note;
    public GameObject trackingParticle;

    public TrackData track;

    [HideInInspector]
    public List<Note> notes;

    public int activeLine = -134;

    public Text title;

    public bool isPlaying = false;

    public float timeline = 0;
    public float timeline2 = 0;
    public List<float> endPattern = new();

    private void Start()
    {
        if (track != null)
        {
            StartCoroutine(_start());
        }
    }

    IEnumerator _start()
    {
        timeline = 0;
        for (int i = 3; i > 0; i--)
        {
            title.text = i.ToString();
            title.color = Color.white;
            yield return new WaitForSeconds(1);
        }

        title.text = "";
        soundManager.Play(track.music);
        isPlaying = true;
    }

    private void Update()
    {
        //cool += Time.deltaTime;

        /*if (cool > delay)
        {
            cool = 0;
            delay = Random.Range(1, 2f);

            var _note = Instantiate(note);
            _note.transform.SetParent(noteLine.transform, false);

            _note.transform.localPosition = new Vector2(Random.Range(-700, 700), 700);

            notes.Add(_note);
        }*/

        if (isPlaying)
        {
            timeline += Time.deltaTime;

            timeline2 = Mathf.Floor(timeline * 10) / 10;

            //if ((Mathf.Floor(timeline * 10) / 10).Equals(1.1f)) Debug.Log((Mathf.Floor(timeline * 10) / 10).Equals(1.1f));
            for (int i = 0; i < track.pattern.patterns.Count; i++)
            {
                //if ((track.MathperfectTime(track.pattern.patterns[i]) == Mathf.Floor(timeline * 10) / 10)) Debug.Log(track.MathperfectTime(track.pattern.patterns[i]) + "_" + Mathf.Floor(timeline * 10) / 10 + "__" + (track.MathperfectTime(track.pattern.patterns[i]) == Mathf.Floor(timeline * 10) / 10));
            }
            var pattern = track.pattern.patterns.Find((v) => track.MathperfectTime(v).Equals(Mathf.Floor(timeline * 10) / 10) && !endPattern.Contains(v.timeline));
            if (pattern != null)
            {
                var _note = Instantiate(note);
                _note.transform.SetParent(noteLine.transform, false);

                _note.transform.position = new Vector2(pattern.x, 0);
                _note.transform.localPosition = new Vector2(_note.transform.localPosition.x, 700);
                _note.speed = track.pattern.patternSpeed;

                notes.Add(_note);

                endPattern.Add(pattern.timeline);
            }
        }
    }

    public void MoveDown()
    {
        track.structure.transform.DOMove(new Vector2(track.structure.transform.position.x, track.structure.transform.position.y - 3f), 0.2f);
    }
    public void Moveleft(float distance)
    {
        player.transform.DOMove(new Vector2(player.transform.position.x - distance, player.transform.position.y), 0.2f);
    }
    public void MoveRight(float distance)
    {
        player.transform.DOMove(new Vector2(player.transform.position.x + distance, player.transform.position.y), 0.2f);
    }
}
