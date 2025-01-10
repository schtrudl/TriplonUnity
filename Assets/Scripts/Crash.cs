using UnityEngine;

public class Crash : MonoBehaviour
{
    public GameObject endMenu;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " crashed into Trail");

        if (endMenu != null)
        {
            endMenu.SetActive(true);
            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.StopTimer();
            }
            GameObject.Find("Audio").GetComponent<Audio>().EndFx();
            // Pause the game when the menu is shown
            Time.timeScale = 0; // Pauses the game
        }
        else
        {
            Debug.LogWarning("EndMenu GameObject is not assigned in the Inspector!");
        }
    }
}
