using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESC_command : MonoBehaviour
{
    // About UI
    public Button continueButton;
    public Button menuButton;
    public Text countdownText;

    // CountDown and InGame?
    private bool isPaused = false;
    private bool countdownActive = false;
    private float countdownDuration = 3f;
    private float countdownTimer = 0f;

    private void Start()
    {
        // Reset UI
        countdownText.gameObject.SetActive(false);

        // Button Click Event
        continueButton.onClick.AddListener(OnContinueButtonClick);
        menuButton.onClick.AddListener(OnMenuButtonClick);
    }

    private void Update()
    {
        // if Press ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        // if Game is Pausing?
        continueButton.gameObject.SetActive(isPaused);
        menuButton.gameObject.SetActive(isPaused);

        // if Countdowning
        if (countdownActive)
        {
            countdownTimer -= Time.unscaledDeltaTime;
            int countdownValue = Mathf.CeilToInt(countdownTimer);
            countdownText.text = countdownValue.ToString();

            // End of Countdown
            if (countdownTimer <= 0)
            {
                countdownText.gameObject.SetActive(false);
                Time.timeScale = 1f; // Time Resume
                countdownTimer = 0f;
                countdownActive = false;
                SetButtonsInteractable(true); // Button Active True
                AudioListener.pause = false; // Sound Play
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Time Stop
        AudioListener.pause = true; // Sound Stop
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Time Resume
        AudioListener.pause = false; // Sound Play
    }

    private void OnContinueButtonClick()
    {
        countdownTimer = countdownDuration;
        countdownText.gameObject.SetActive(true);
        countdownActive = true;
        SetButtonsInteractable(false); // Button Active False
    }

    private void OnMenuButtonClick()
    {
        Time.timeScale = 1f; // Time Resume
        AudioListener.pause = false; // Sound Play
        SceneManager.LoadScene("Menu"); // Go to "Menu"
    }

    // Button Interact
    private void SetButtonsInteractable(bool interactable)
    {
        continueButton.interactable = interactable;
        menuButton.interactable = interactable;
    }
}
