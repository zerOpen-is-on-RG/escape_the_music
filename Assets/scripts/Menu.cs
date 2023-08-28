using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public SoundManager soundManager;
    void Start()
    {
        soundManager.Play("music.menu");
    }
}
