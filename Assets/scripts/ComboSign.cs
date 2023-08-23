using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSign : MonoBehaviour
{
    GameManager gameManager;

    Text text;
    public Text indicator;

    bool combing = false;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                color.a -= 0.2f * Time.deltaTime;
            }

            text.color = color;
            indicator.text = gameManager.combo + " combo";
        }
    }

    public void OnCombo(int type)
    {
        gameObject.SetActive(true);
        StartCoroutine(_onCombo(type));
    }

    IEnumerator _onCombo(int type)
    {
        combing = true;
        if (type == -1)
        {
            text.text = "MISS";
            text.color = Color.gray;
        }
        else if (type == 0)
        {
            text.text = "NOT BAD";
            text.color = Color.red;
            gameManager.score += 50;
        }
        else if (type == 1)
        {
            text.text = "GOOD";
            text.color = Color.yellow;
            gameManager.score += 200;
        }
        else if (type == 2)
        {
            text.text = "GREAT!";
            text.color = Color.green;
            gameManager.score += 300;
        }
        else if (type == 3)
        {
            text.text = "PERFECT!";
            text.color = new Color(1, 0.7f, 0, 1);
            gameManager.score += 500;
        }
        Debug.Log(text.color);
        transform.localScale = Vector3.one;
        transform.DOScale(new Vector2(1.1f, 1.1f), 0.1f);

        yield return new WaitForSeconds(0.1f);

        transform.DOScale(Vector2.one, 0.1f);

        combing = false;
    } 
}
