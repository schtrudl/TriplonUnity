using UnityEngine;
using UnityEngine.UI;

public class Multi : MonoBehaviour
{
    public GameObject prefab;
    private GameObject player2;
    public Button playButton;
    public void OnMultiPlayerButtonClick()
    {
        AddPlayer2();
        EnablePlayButton();
    }

    public void OnSinglePlayerButtonClick()
    {
        ResetPlayer2();
        EnablePlayButton();
    }

    private void EnablePlayButton()
    {
        if (playButton != null)
        {
            playButton.interactable = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //AddPlayer2();
        if (playButton != null)
        {
            playButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds second player
    /// </summary>
    public void AddPlayer2()
    {
        if (player2 == null)
        {
            player2 = Instantiate(prefab);
            player2.name = prefab.name;
        }
    }
    private void ResetPlayer2()
    {
        if (player2 != null)
        {
            Destroy(player2);
            player2 = null;
        }
    }
}
