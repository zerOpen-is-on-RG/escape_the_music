using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp = 100;
    // Start is called before the first frame update

    public float downCount = 0;
    public bool down = false;

    public readonly Vector2 default_pos = new Vector2(4.56f, -1.58f);

    public ObstacleSign obstacleSign;

    GameManager gameManager;
    Animator animator;
    SpriteRenderer renderer_;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        renderer_ = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    bool CheckIsGround()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, -2, 0), 0.4f, LayerMask.GetMask("tile"));


        return cols.Length > 0;
    }
    public bool CheckIsChest()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, -1, 0), 0.5f, LayerMask.GetMask("chest"));

        if (cols.Length < 1) return false;

        if (cols[0].GetComponent<Animator>().GetBool("open") == true) return false;

        return cols.Length > 0;
    }
    public Animator getChest()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, -1, 0), 0.5f, LayerMask.GetMask("chest"));

        if (cols.Length < 1) return null;

        var result = cols[0].GetComponent<Animator>();

        if (result.GetBool("open") == true) return null;

        return result;
    }
    public void MoveMotion(bool direction)
    {
        animator.SetBool("moving", true);
        renderer_.flipX = direction;
        StartCoroutine(_moveMotion());
    }
    IEnumerator _moveMotion() {
        yield return new WaitForSeconds(0.3f);

        animator.SetBool("moving", false);

    }
    // Update is called once per frame
    void Update()
    {
        if (down && gameManager.detecting)
        {
            if (!CheckIsGround())
            {
                down = false;
                gameManager.MoveUp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("coin"))
        {
            gameManager.soundManager.Play("effect.coin");
            collision.gameObject.GetComponent<Animator>().SetBool("get", true);
            Destroy(collision.gameObject, 0.5f);

            obstacleSign.transform.position = transform.position + new Vector3(0.5f, 0);
            obstacleSign.onSignGood(200);

            gameManager.score += 200;
            gameManager.collectedCoins++;
        }
        else if (((1 << collision.gameObject.layer) & LayerMask.GetMask("obstacle")) != 0)
        {
            if (transform.position.y - 1.5f > collision.transform.position.y) return;

            gameManager.soundManager.Play("effect.crash");
            obstacleSign.transform.position = transform.position;
            obstacleSign.onSign(300);

            gameManager.score -= 300;
        }
    }
}
