using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    public Slider boostSlider; 
    private PlayerController otherScript; 
    public string variableName = "boost";
    private string parentName;

    void Start()
    {
        // Find the other script on the parent GameObject
        otherScript = GetComponentInParent<PlayerController>();

        if (boostSlider == null)
        {
            // beacuse Boost1 is linke in Scene
            boostSlider = GameObject.Find("Boost2")?.GetComponent<Slider>();
        }

        if (boostSlider == null)
        {
            Debug.LogError("BoostSlider not assigned in BoostBar script!");
        }

        if (otherScript == null)
        {
            Debug.LogError("OtherScript not found on parent!");
        }
    }

    void Update()
    {
        if (boostSlider != null && otherScript != null)
        {
            // Assuming the monitored variable is a float between 0 and 1
            boostSlider.value = Mathf.Clamp01(otherScript.boostRemainingTime/otherScript.boostFullTime);

            // If boost is 0, optionally disable the BoostBar
            boostSlider.gameObject.SetActive(otherScript.boostRemainingTime > 0);
        }
    }
}
