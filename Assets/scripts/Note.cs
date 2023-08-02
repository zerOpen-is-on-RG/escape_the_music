using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float speed;
    Vector2 startPos;
    bool isPressed;

    Image img;
    Text sign;

    GameManager _gameManager;

    int touchId;

    private void Start()
    {
        img = GetComponent<Image>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sign = transform.Find("noteSign").GetComponent <Text>();
    }

    private void Update()
    {
        Color col = img.color;

        if (transform.localPosition.y > _gameManager.activeLine)
        {
            col = Color.gray;
        } else
        {
            col = new Color(0, 206, 255);
        }

        if (isPressed)
        {
            Vector2 pos = (touchId == -1) ? Input.mousePosition : Input.GetTouch(touchId).position;
            var point = Camera.main.ScreenToWorldPoint(pos);
            transform.position = new Vector2(point.x, transform.position.y);

            col = Color.yellow;

            sign.transform.position = startPos;

            if (startPos.x < transform.position.x - 1f)
            {
                sign.text = "¢º";
            }
            else if (startPos.x > transform.position.x + 1f)
            {
                sign.text = "¢¸";
            }
            else
            {
                sign.text = "¡å";
            }

            sign.text += "\n" + Mathf.Floor(Vector2.Distance(transform.position, startPos) / 4 * 10)/10;

            transform.localScale = new Vector2(1.1f, 1.1f);
        }
        else
        {
            col.a = 0.7f;

            sign.text = "";

            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

            transform.localScale = new Vector2(1f, 1f);
        }

        img.color = col;

        if (transform.localPosition.y < -702) Destory();
    }

    public void Destory(float del_time = 0)
    {
        _gameManager.notes.Remove(this);
        Destroy(gameObject, del_time);
    }

    public void NoteDown() {
        startPos = transform.position;
        touchId = Input.touchCount - 1;
        isPressed = true;
    }

    public void NoteUp()
    {
        isPressed = false;

        if (startPos.x < transform.position.x - 1f) {
            Debug.Log("Right");
            _gameManager.MoveRight(Vector2.Distance(transform.position, startPos) / 4);
        } else if (startPos.x > transform.position.x + 1f)
        {
            Debug.Log("Left");
            _gameManager.Moveleft(Vector2.Distance(transform.position, startPos) / 4);
        } else
        {
            Debug.Log("Click");
            _gameManager.MoveDown();
        }

        transform.position = startPos;

        Destory(0.1f);
    }
}
