using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleSign : MonoBehaviour
{
    Text text;

    public bool combing = false;
    private void Start()
    {
        text = GetComponent<Text>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!combing)
        {
            Color color = text.color;
            if (color.a > 0)
            {
                color.a -= 256f * Time.deltaTime;
            }

            text.color = color;
        }
    }

    public void onSignGood(int score)
    {
        gameObject.SetActive(true);
        StartCoroutine(_onSignGood(score));
    }

    IEnumerator _onSignGood(int score)
    {
        combing = true;
        transform.localScale = Vector3.one;
        transform.DOScale(new Vector2(0.6f, 0.6f), 0.1f);

        text.color = new Color(0, 217, 255, 255);

        yield return new WaitForSeconds(0.1f);

        transform.DOScale(new Vector2(0.5f, 0.5f), 0.1f);

        text.text = "+" + score + "\r\n           score!";

        combing = false;
    }

    public void onSign(int score)
    {
        gameObject.SetActive(true);
        StartCoroutine(_onSign(score));
    }

    IEnumerator _onSign(int score)
    {
        combing = true;
        transform.localScale = Vector3.one;
        transform.DOScale(new Vector2(0.6f, 0.6f), 0.1f);

        text.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        transform.DOScale(new Vector2(0.5f, 0.5f), 0.1f);

        text.text = "-" + score + "\r\n         score!";

        combing = false;
    } 
}
