using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SelectMusic : MonoBehaviour
{
    public TrackSet trackSet;
    UIDocument ui;

    TrackData trackData;
    void Start()
    {
        ui = GetComponent<UIDocument>();

        ui.rootVisualElement.Q<Button>("musicBackground").RegisterCallback<ClickEvent>(Select);

        string nam = PlayerPrefs.GetString("selectedMusic");
        var f = trackSet.FindByName(nam);

        if (nam == null || f == null)
        {
            PlayerPrefs.SetString("selectedMusic", trackSet.trackData[0]._name);

            trackData = trackSet.trackData[0];
        } else
        {
            trackData = f;
        }

        Debug.Log(trackData.displayName);
    }

    public void DisplayMusic()
    {

    }

    void Select(ClickEvent ev)
    {
        SceneManager.LoadScene("GameScreen");
    }
}
