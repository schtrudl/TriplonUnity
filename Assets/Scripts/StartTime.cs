using UnityEngine;
using UnityEngine.UI;

public class StartTime : MonoBehaviour
{
    public Button playButton;

    public void Starttime()
    {
        Time.timeScale = 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
