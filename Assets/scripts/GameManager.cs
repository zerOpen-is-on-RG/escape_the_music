using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject structures;
    public Player player;
    public Canvas noteLine;
    public Note note;

    [HideInInspector]
    public List<Note> notes;

    private float cool = 0;
    private float delay = 2;

    public int activeLine = -134;

    private void Update()
    {
        cool += Time.deltaTime;

        if (cool > delay)
        {
            cool = 0;
            delay = Random.Range(1, 2f);

            var _note = Instantiate(note);
            _note.transform.SetParent(noteLine.transform, false);

            _note.transform.localPosition = new Vector2(Random.Range(-700, 700), 700);

            notes.Add(_note);
        }
    }

    public void MoveDown()
    {
        structures.transform.DOMove(new Vector2(structures.transform.position.x, structures.transform.position.y - 3f), 0.2f);
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
