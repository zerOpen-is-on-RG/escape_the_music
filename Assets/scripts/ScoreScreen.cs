using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject starSet;
    public GameObject scorePlayer;
    public List<Image> stars;
    public Text trackName;
    public Text trackDifficulty;
    public Text score;
    public Text maxCombo;
    public Text collectedStars;
    public Text collectedCoins;
    public Text perfect;
    public Text great;
    public Text good;
    public Text notbad;
    public Text miss;
    public Text moreStars;
    public List<Text> bests;
    public GameObject dataPanel;
    public GameObject cutline;

    public void Display(bool best)
    {
        gameObject.SetActive(true);
        scorePlayer.transform.localPosition = new Vector2(0, -1500);
        dataPanel.transform.localPosition = new Vector2(0, -1500);

        trackName.text = gameManager.track.displayName.ToString();
        trackDifficulty.text = "normal";
        score.text = "점수:                 " + gameManager.score.ToString();
        maxCombo.text = "최대 콤보:         " + gameManager.maxCombo.ToString();
        collectedStars.text = "수집한 상자:      " + gameManager.collectedStars.ToString();
        collectedCoins.text = "수집한 코인:      " + gameManager.collectedCoins.ToString();
        perfect.text = "perfect:          " + gameManager.perfect.ToString();
        great.text = "great:              " + gameManager.great.ToString();
        good.text = "good:               " + gameManager.good.ToString();
        notbad.text = "not bad:          " + gameManager.notbad.ToString();
        miss.text = "miss:                " + gameManager.miss.ToString();
        moreStars.text = "";

        if (gameManager.collectedStars > 3)
        {
            moreStars.text = "+" + (gameManager.collectedStars - 3);
        }

        stars.ForEach((v) => {
            var color = v.color;
            color.a = 0;
            v.color = color;
        });

        StartCoroutine(_display(best));
    }
    IEnumerator _display(bool best)
    {
        gameManager.soundManager.Play("effect.shot");
        yield return new WaitForSeconds(0.2f);
        cutline.transform.DOScale(Vector2.zero, 0.1f);
        gameManager.screen.transform.DOScale(Vector2.zero, 0.1f);
        gameManager.tv.transform.DOScale(Vector2.zero, 0.1f);
        gameManager.comboSign.transform.DOScale(Vector2.zero, 0.1f);
        yield return new WaitForSeconds(1f);

        gameManager.soundManager.Play("music.score");
        gameManager.soundManager._tracks[3].time = 0;
        yield return new WaitForSeconds(0.3f);

        scorePlayer.transform.localScale = Vector2.zero;
        scorePlayer.transform.DOLocalMove(new Vector2(11, 118), 0.5f);
        scorePlayer.transform.DOScale (new Vector2(2.5f, 2.5f), 0.5f);

        yield return new WaitForSeconds(0.4f);
        scorePlayer.transform.DOLocalRotate(new Vector3(0, 0, -25), 1.3f);

        yield return new WaitForSeconds(1.3f);
        scorePlayer.transform.DOLocalRotate(Vector3.zero, 0.3f);
        scorePlayer.transform.DOLocalMove(new Vector2(-498, 0), 0.3f);
        scorePlayer.transform.DOScale(new Vector2(2f, 2f), 0.3f);

        yield return new WaitForSeconds(0.35f);
        dataPanel.transform.localPosition = new Vector2(-425, 0);
        dataPanel.transform.localScale = new Vector2(0, 0.8f);
        dataPanel.transform.DOLocalMove(Vector2.zero, 1f);
        dataPanel.transform.DOScale(Vector2.one, 1f);

        yield return new WaitForSeconds(2f);

        for (int i = 0; i <stars.Count; i++)
        {
            var v = stars[i];

            if (i < gameManager.collectedStars)
            {
                StartCoroutine(shootStar(v, true));
            } else
            {
                StartCoroutine(shootStar(v, false));
            }

            yield return new WaitForSeconds(0.25f);
        }

        if (!best) yield break;
        gameManager.soundManager.Play("effect.bum", true);
        gameManager.soundManager.Play("effect.bum", false);
        gameManager.soundManager.Play("effect.bum2", false);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < bests.Count; i++)
        {
            var v = bests[i];
            if (v != null)
            {
                v.gameObject.SetActive(true);
                v.transform.localScale = Vector2.zero;
                v.transform.DOScale(Vector2.one, 0.2f);
            }

            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator shootStar(Image obj, bool yellow)
    {
        var star = Instantiate(obj);
        star.transform.SetParent(starSet.transform, false);
        star.transform.localPosition = new Vector2(709, 663);
        star.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 100));
        star.transform.localScale = new Vector2(1.5f, 1.5f);

        star.color = Color.gray;

        star.transform.DOLocalMove(obj.transform.localPosition, 0.3f);
        star.transform.DOLocalRotate(Vector3.zero, 0.3f);
        star.transform.DOScale(new Vector2(1.1f, 1.1f), 0.3f);
        gameManager.soundManager.Play("effect.tab");
        yield return new WaitForSeconds(0.3f);

        if (yellow) star.color = Color.yellow;

        yield return new WaitForSeconds(0.1f);
        star.transform.DOScale(Vector3.one, 0.1f);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
