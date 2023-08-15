using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SelectMusic : MonoBehaviour
{
    UIDocument ui;
    void Start()
    {
        ui = GetComponent<UIDocument>();

        ui.rootVisualElement.Q<Button>("musicBackground").RegisterCallback<ClickEvent>(Select);
    }

    void Select(ClickEvent ev)
    {
        SceneManager.LoadScene("GameScreen");
    }
}
