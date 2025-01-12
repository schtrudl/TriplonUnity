using TMPro;
using UnityEngine;

public class End : MonoBehaviour
{
    public void end(string msg)
    {
        gameObject.SetActive(true);
        Transform gameOverMenuTransform = gameObject.transform.Find("GameOverMenu");
        if (gameOverMenuTransform == null)
        {
            Debug.LogError("GameOverMenu GameObject is missing inside EndMenu!");
            return;
        }
        // Navigate to Background -> Image -> DeathReason
        Transform deathReasonTransform = gameOverMenuTransform
            .Find("Image/DeathReason");
        if (deathReasonTransform == null)
        {
            Debug.LogError("DeathReason GameObject is missing inside GameOverMenu!");
            return;
        }
        TextMeshProUGUI reasonText = deathReasonTransform.GetComponent<TextMeshProUGUI>();
        if (reasonText == null)
        {
            Debug.LogError("No TextMeshProUGUI component found on DeathReason GameObject!");
            return;
        }
        reasonText.text = msg;
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.StopTimer();
        }
        GameObject.Find("Audio").GetComponent<Audio>().EndFx();
        Time.timeScale = 0;
    }
}
