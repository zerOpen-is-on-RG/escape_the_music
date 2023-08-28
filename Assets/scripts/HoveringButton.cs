using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringButton : MonoBehaviour
{
    public Vector2 before = new Vector2(0.92f, 0.8f);
    public Vector2 after = new Vector2(0.97f, 0.85f);
    public void OnHover()
    {
        transform.DOKill();
        transform.localScale = before;
        transform.DOScale(after, 0.2f);
    }

    public void OutHover()
    {
        transform.DOKill();
        transform.localScale = after;
        transform.DOScale(before, 0.2f);
    }
}
