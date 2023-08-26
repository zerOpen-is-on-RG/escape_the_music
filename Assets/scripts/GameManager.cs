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
    public GameObject blur;
    public GameObject screen;
    public GameObject tv;
    public TrackSet trackSet;
    public Vcam vcam;
    public ComboSign comboSign;
    public Text scoreIndicator;

    public TrackData track;

    public int score;
    public int combo = 0;

    float autoScore = 0;

    [HideInInspector]
    public List<Note> notes;

    public int activeLine = -134;

    public Text title;

    public bool isPlaying = false;
    public bool detecting = false;

    public float timeline = 0;
    public float forceTimeline = 0;
    public List<float> endPattern = new();



    private void Start()
    {
        Application.targetFrameRate = 60;

        track = trackSet.FindByName(PlayerPrefs.GetString("selectedMusic"));
        if (track != null)
        {
            StartCoroutine(_start());
        }
    }

    IEnumerator _start()
    {
        screen.transform.localScale = Vector2.zero;
        tv.transform.localPosition = new Vector2(0, -1000);
        tv.transform.DOLocalMove(Vector2.zero, 0.8f);
        tv.transform.DOScale(new Vector2(1.1f, 1.1f), 0.8f);
        yield return new WaitForSeconds(0.8f);

        tv.transform.DOScale(new Vector2(1f, 1f), 0.2f);
        yield return new WaitForSeconds(0.2f);

        screen.transform.DOScale(Vector2.one, 3f);

        timeline = 0;
        for (int i = 5; i > 0; i--)
        {
            title.text = i.ToString();
            title.color = Color.white;
            soundManager.Play("effect.click");
            yield return new WaitForSeconds(1);
        }

        title.text = "";

        if (forceTimeline > 0)
        {
            for (float i = 0; i <= forceTimeline; i+=0.2f)
            {
                track.effect.EffectUpdate(timeline);

                timeline += 0.2f;
            }
        }

        yield return new WaitForSecondsRealtime(1);

        soundManager.Play(track.music);
        soundManager._tracks[3].time = forceTimeline;
        isPlaying = true;
        detecting = false;
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

                _note.type = pattern.noteType;

                _note.transform.position = new Vector2(pattern.x, 0);
                _note.transform.localPosition = new Vector2(_note.transform.localPosition.x, 700);
                _note.speed = track.pattern.patternSpeed;

                notes.Add(_note);

                endPattern.Add(pattern.timeline);
            }

            autoScore += Time.deltaTime;

            if (autoScore > 1)
            {
                autoScore = 0;
                score += 7;
            }

            track.effect.EffectUpdate(timeline);

            scoreIndicator.text = score.ToString();
        }
    }

    public void MoveDown()
    {
        StartCoroutine(_moveDown());
    }
    IEnumerator _moveDown()
    {
        player.down = false;
        track.structure.transform.DOLocalMove(new Vector2(track.structure.transform.localPosition.x, track.structure.transform.localPosition.y - 3f), 0.2f);

        yield return new WaitForSeconds(0.3f);
        player.down = true;
    }
    public void MoveUp()
    {
        track.structure.transform.DOMove(new Vector2(track.structure.transform.position.x, track.structure.transform.position.y + 3f), 0.15f);
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
