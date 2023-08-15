using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    // Start is called before the first frame update

    public float downCount = 0;
    public bool down = false;

    public readonly Vector2 default_pos = new Vector2(4.56f, -2.04f);

    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    bool CheckIsGround()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, -1, 0), 0.4f, LayerMask.GetMask("tile"));


        return cols.Length > 0;
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
}
