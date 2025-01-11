using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject endMenu;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalTimeText;

    private bool isSinglePlayer = false;
    private bool isTimerRunning = false;
    private float elapsedTime = 0f;

    public void SelectSinglePlayer(bool single)
    {
        if (single)
        {
            isSinglePlayer = true;
            Debug.Log("Single Player mode selected!");
        }
        else
        {
            isSinglePlayer = false;
        }
    }
    public void StartTimer()
    {
        if (isSinglePlayer)
        {
            isTimerRunning = true;
            elapsedTime = 0f;
            Debug.Log("Starting Single Player timer!");
        }
    }

    public void StopTimer()
    {
        if (isTimerRunning)
        {
            Debug.Log("Timer stopped!");

            if (finalTimeText != null)
            {
                finalTimeText.text = $"Your Survival Time: {timerText.text}";
            }
        }
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;

            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
