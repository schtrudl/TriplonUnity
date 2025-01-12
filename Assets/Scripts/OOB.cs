using TMPro;
using UnityEngine;

public class OOB : MonoBehaviour
{
    public GameObject endMenu;
    public TextMeshProUGUI reasonText;

    private void Start()
    {
        if (endMenu == null)
        {
            endMenu = GameObject.Find("EndMenu");

            if (endMenu != null)
            {
                Debug.Log("EndGame GameObject found!");
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("" + other.gameObject.name + " is out of bounds (probably hit the wall)");
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
            reasonText.text = $"You hit the wall!";
        }

        else
        {
            Debug.LogWarning("EndMenu GameObject is not assigned in the Inspector!");
        }
    }
}
