using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SelectMusic : MonoBehaviour
{
    public TrackSet trackSet;
    UIDocument ui;
    public StageDB stageDB;

    TrackData trackData;
    public Menu menu;
    void Start()
    {
        ui = GetComponent<UIDocument>();

        ui.rootVisualElement.Q<Button>("musicBackground").RegisterCallback<ClickEvent>(Select);
        ui.rootVisualElement.Q<Button>("exitButton").RegisterCallback<ClickEvent>(CloseMusic);
    }

    public void Fade()
    {
        StartCoroutine(_fade());
    }

    IEnumerator _fade()
    {
        var fade = ui.rootVisualElement.Q<VisualElement>("fade");
        fade.style.display = DisplayStyle.Flex;

        yield return new WaitForSeconds(0.2f);
        fade.AddToClassList("fadeOut");

        yield return new WaitForSeconds(0.4f);
        fade.style.display = DisplayStyle.None;
        fade.RemoveFromClassList("fadeOut");
    }

    public void DisplayMusic()
    {
        Fade();

        string nam = PlayerPrefs.GetString("selectedMusic");
        var f = trackSet.FindByName(nam);

        if (nam == null || f == null)
        {
            PlayerPrefs.SetString("selectedMusic", trackSet.trackData[0]._name);

            trackData = trackSet.trackData[0];
        }
        else
        {
            trackData = f;
        }

        menu.soundManager.Play("effect.click");
        ui.rootVisualElement.Q<VisualElement>("selectMusicPanel").AddToClassList("selectMusicIn");

        DataResult result = stageDB.GetDataByName(trackData._name);

        if (!stageDB.HasData(trackData._name))
        {
            result.name = trackData._name;
            result.score = 0;
            result.collectedStars = 0;
        }

        ui.rootVisualElement.Q<Label>("nowTopic").text = trackData.displayName;
        ui.rootVisualElement.Q<Label>("musicName").text = trackData.displayName;
        ui.rootVisualElement.Q<Label>("authorName").text = trackData.author;
        ui.rootVisualElement.Q<Label>("score").text = result.score.ToString();
        ui.rootVisualElement.Q<Label>("score").text = result.score.ToString();
        ui.rootVisualElement.Q<VisualElement>("musicBackground").style.backgroundImage = new StyleBackground(trackData.logoBackground);

        if (result.collectedStars < 1) ui.rootVisualElement.Q<VisualElement>("stars1").style.unityBackgroundImageTintColor = Color.gray;
        else ui.rootVisualElement.Q<VisualElement>("stars1").style.unityBackgroundImageTintColor = Color.yellow;

        if (result.collectedStars < 2) ui.rootVisualElement.Q<VisualElement>("stars2").style.unityBackgroundImageTintColor = Color.gray;
        else ui.rootVisualElement.Q<VisualElement>("stars2").style.unityBackgroundImageTintColor = Color.yellow;

        if (result.collectedStars < 3) ui.rootVisualElement.Q<VisualElement>("stars3").style.unityBackgroundImageTintColor = Color.gray;
        else ui.rootVisualElement.Q<VisualElement>("stars3").style.unityBackgroundImageTintColor = Color.yellow;

        if (result.collectedStars < 4) ui.rootVisualElement.Q<Label>("moreStar").text = "";
        else ui.rootVisualElement.Q<Label>("moreStar").text = "+" + (result.collectedStars - 3);

        menu.soundManager.Stop(4);
        menu.soundManager.Play(trackData.music, true);

        int i = trackSet.IndexByName(trackData._name);

        TrackData next = trackSet.FindByIndex(i + 1);
        TrackData before = trackSet.FindByIndex(i - 1);

        if (next != null)
        {
            ui.rootVisualElement.Q<Label>("upSign").style.display = DisplayStyle.Flex;
            ui.rootVisualElement.Q<Label>("topTopic").style.display = DisplayStyle.Flex;
            ui.rootVisualElement.Q<Label>("topTopic").text = next.displayName;
        } else
        {
            ui.rootVisualElement.Q<Label>("upSign").style.display = DisplayStyle.None;
            ui.rootVisualElement.Q<Label>("topTopic").style.display = DisplayStyle.None;
        }

        if (before != null)
        {
            ui.rootVisualElement.Q<Label>("downSign").style.display = DisplayStyle.Flex;
            ui.rootVisualElement.Q<Label>("bottomTopic").style.display = DisplayStyle.Flex;
            ui.rootVisualElement.Q<Label>("bottomTopic").text = before.displayName;
        } else
        {
            ui.rootVisualElement.Q<Label>("downSign").style.display = DisplayStyle.None;
            ui.rootVisualElement.Q<Label>("bottomTopic").style.display = DisplayStyle.None;
        }

        ui.rootVisualElement.Q<Button>("nextButton").RegisterCallback<ClickEvent>(UpMusic);
        ui.rootVisualElement.Q<Button>("prevButton").RegisterCallback<ClickEvent>(DownMusic);
    }

    public void UpMusic(ClickEvent ev) {
        int i = trackSet.IndexByName(trackData._name);

        TrackData next = trackSet.FindByIndex(i + 1);
        Debug.Log(next);

        if (next == null) return;

        PlayerPrefs.SetString("selectedMusic", next._name);

        DisplayMusic();
    }

    public void DownMusic(ClickEvent ev)
    {
        int i = trackSet.IndexByName(trackData._name);

        TrackData next = trackSet.FindByIndex(i - 1);
        if (next == null) return;

        PlayerPrefs.SetString("selectedMusic", next._name);

        DisplayMusic();
    }

    void CloseMusic(ClickEvent ev)
    {
        ui.rootVisualElement.Q<VisualElement>("selectMusicPanel").RemoveFromClassList("selectMusicIn");
        menu.soundManager.Play("effect.click");
        menu.soundManager.Stop(5);
        menu.soundManager.Play("music.menu");
    }

    void Select(ClickEvent ev)
    {
        stageDB.Close();
        SceneManager.LoadScene("GameScreen");
    }
}
