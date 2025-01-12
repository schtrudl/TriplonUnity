using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button pauseButton;

    public void ShowPauseButton()
    {
        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(true);
        }
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);

        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(false);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);

        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
    public void Start()
    {
        pauseMenuUI.SetActive(false);
    }

}
