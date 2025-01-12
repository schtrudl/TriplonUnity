using UnityEngine;
using UnityEngine.UI;

public class Multi : MonoBehaviour
{
    public GameObject prefab;
    private GameObject player2;
    public Button playButton;
    public Button singlePlayerButton;
    public Button multiplayerButton;

    public Color selectedColor = Color.grey;
    public Color defaultColor = Color.white;
    public void OnMultiPlayerButtonClick()
    {
        AddPlayer2();
        EnablePlayButton();
        UpdateButtonColors(multiplayerButton);
    }

    public void OnSinglePlayerButtonClick()
    {
        ResetPlayer2();
        EnablePlayButton();
        UpdateButtonColors(singlePlayerButton);
    }

    private void UpdateButtonColors(Button selectedButton)
    {
        ResetButtonColors();
        if (selectedButton != null)
        {
            var colors = selectedButton.colors;
            colors.normalColor = selectedColor;
            selectedButton.colors = colors;
        }
    }

    private void ResetButtonColors()
    {
        ResetButtonColor(singlePlayerButton);
        ResetButtonColor(multiplayerButton);
    }

    private void ResetButtonColor(Button button)
    {
        if (button != null)
        {
            var colors = button.colors;
            colors.normalColor = defaultColor;
            button.colors = colors;
        }
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
        Time.timeScale = 0;
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
            player2.AddComponent<BoostBar>();
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
