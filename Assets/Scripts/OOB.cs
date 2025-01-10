using UnityEngine;

public class OOB : MonoBehaviour
{
    public GameObject endMenu;
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
