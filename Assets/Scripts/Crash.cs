using TMPro;
using UnityEngine;

public class Crash : MonoBehaviour
{
    public GameObject endMenu;
    public TextMeshProUGUI reasonText;

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

            Time.timeScale = 0;
            
            if (reasonText != null)
            {
                reasonText.text = $"You hit the trail!";
            }
        }
        else
        {
            Debug.LogWarning("EndMenu GameObject is not assigned in the Inspector!");
        }
    }
}
