using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float speed;
    public ParticleSystem effect;
    Vector2 startPos;
    bool isPressed;
    bool broken = false;

    public string type;

    public Image img;

    public Sprite def;
    public Sprite hold;
    public Sprite right;
    public Sprite left;

    public Sprite upIcon;
    public Sprite downIcon;
    public Sprite horizontalicon;

    GameManager _gameManager;

    public Image icon;

    int touchId;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!broken)
        {
            Color col = img.color;

            if (type.Equals("up"))
            {
                col = new Color(0, 206, 255);

                icon.sprite = upIcon;
            }
            else if (type.Equals("down"))
            {
                col = new Color(0, 206, 255);

                icon.sprite = downIcon;
            }
            else if (type.Equals("horizontal1"))
            {
                col = Color.yellow;
                icon.sprite = horizontalicon;
            }
            else if (type.Equals("horizontal2"))
            {
                col = new Color(255, 106, 0);
                icon.sprite = horizontalicon;
            }
            else
            {
                col = Color.white;
                icon.color = new Color(0, 0, 0, 0);
            }

            if (transform.localPosition.y > _gameManager.activeLine)
            {
                col.a = 0.4f;
            }
            else
            {
                col.a = 0.9f;
            }

            if (isPressed)
            {
                Vector2 pos = (touchId == -1) ? Input.mousePosition : Input.GetTouch(touchId).position;
                var point = Camera.main.ScreenToWorldPoint(pos);
                transform.position = new Vector2(point.x, transform.position.y);

                transform.localScale = new Vector2(1.1f, 1.1f);

                if (type.StartsWith("horizontal"))
                {
                    if (startPos.x < transform.position.x - 1.25f)
                    {
                        img.sprite = right;
                    }
                    else if (startPos.x > transform.position.x + 1.25f)
                    {
                        img.sprite = left;
                    }
                    else
                    {
                        img.sprite = hold;
                    }
                }

                icon.gameObject.SetActive(false);
            }
            else
            {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * 50 * Time.deltaTime);

                transform.localScale = new Vector2(1f, 1f);

                img.sprite = def;
            }

            img.color = col;

            if (transform.localPosition.y < -702)
            {
                Destory_();

                _gameManager.combo = 0;
                _gameManager.comboSign.OnCombo(-1);
            }
        }
    }

    public void Destory_(float del_time = 0)
    {
        _gameManager.notes.Remove(this);
        Destroy(gameObject, del_time);
    }

    public void NoteDown() {
        startPos = transform.position;
        touchId = Input.touchCount - 1;
        isPressed = true;

        if (transform.localPosition.y > _gameManager.activeLine)
        {
            Destory_();

            _gameManager.combo = 0;
            _gameManager.miss++;
            _gameManager.comboSign.OnCombo(-1);

            return;
        }

        _gameManager.soundManager.Play("effect.tab");
        _gameManager.soundManager._tracks[1].time = 0.1f;

        if (type.Equals("default") || type.Equals("up"))
        {
            NoteUp();
        }
    }

    public void NoteUp()
    {
        if (broken) return;

        isPressed = false;
        broken = true;

        if (type == "horizontal1")
        {
            if (startPos.x < transform.position.x - 1.25f)
            {
                _gameManager.MoveRight(1);
            }
            else if (startPos.x > transform.position.x + 1.25f)
            {
                _gameManager.Moveleft(1);
            }
        } else if (type == "horizontal2")
        {
            if (startPos.x < transform.position.x - 1.25f)
            {
                _gameManager.MoveRight(2);
            }
            else if (startPos.x > transform.position.x + 1.25f)
            {
                _gameManager.Moveleft(2);
            }
        } else if (type == "up")
        {
            _gameManager.MoveDown();
        }
        else if (type == "down")
        {
            _gameManager.MoveUp();
        }

        transform.position = startPos;

        var eff = Instantiate(effect, transform.position, Quaternion.identity);

        //eff.transform.DORotate(new Vector3(0, 0, Random.Range(-20f, 20f)), 0.15f);
        _gameManager.combo++;
        _gameManager.score += _gameManager.combo;

        eff.transform.DOScale(new Vector3(2, 0.7f), 0.15f);

        if (Mathf.Abs(transform.localPosition.y - _gameManager.activeLine) <= 150)
        {
            _gameManager.comboSign.OnCombo(3);
            _gameManager.perfect++;
        } else if (Mathf.Abs(transform.localPosition.y - _gameManager.activeLine) <= 200)
        {
            _gameManager.comboSign.OnCombo(2);
            _gameManager.good++;
        }
        else if (Mathf.Abs(transform.localPosition.y - _gameManager.activeLine) <= 250)
        {
            _gameManager.comboSign.OnCombo(1);
            _gameManager.notbad++;
        }
        else
        {
            _gameManager.comboSign.OnCombo(0);
            _gameManager.combo = 0;
            _gameManager.miss++;
        }

        Destory_();
    }
}
