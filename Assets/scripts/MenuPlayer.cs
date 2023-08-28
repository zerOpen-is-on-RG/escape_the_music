using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    SpriteRenderer renderer_;
    Animator animator;
    public Menu menu;
    void Start()
    {
        renderer_ = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void MoveMotion()
    {
        menu.soundManager.Play("effect.click");
        animator.SetBool("moving", true);
        StartCoroutine(_moveMotion());
    }
    IEnumerator _moveMotion()
    {
        yield return new WaitForSeconds(0.3f);

        animator.SetBool("moving", false);

    }
}
