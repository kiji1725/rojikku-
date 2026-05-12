using UnityEngine;
using UnityEngine.UI;
public class Pose : MonoBehaviour
{
    private bool isPaused = false;
    public Button pauseButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        SetButtonColor(new Color(0.4f, 0.8f, 1f));
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SetButtonColor(Color.white);
    }

    void SetButtonColor(Color color)
    {
        ColorBlock colors = pauseButton.colors;
        colors.normalColor = color;
        pauseButton.colors = colors;
    }
}
