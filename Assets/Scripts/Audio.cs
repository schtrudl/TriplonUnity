using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Use `GameObject.Find("/Audio").GetComponent<Audio>()` to obtain global instance of audio
/// </summary>
public class Audio : MonoBehaviour
{
    public AudioSource audioPlayer;
    public AudioClip introMusic;
    public AudioClip ambientMusic;
    public AudioClip discFx;
    public AudioClip endFx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.BgMusic(true);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.I))
        {
            this.BgMusic(true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.BgMusic(false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.DiscFx();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.EndFx();
        }*/
    }

    /// <summary>
    /// Plays end fx once then starts playing previous music again
    /// </summary>
    public void EndFx()
    {
        audioPlayer.Stop();
        audioPlayer.PlayOneShot(endFx, 0.25F / audioPlayer.volume);
        audioPlayer.PlayDelayed(endFx.length);
    }

    /// <summary>
    /// Plays disc fx once
    /// </summary>
    public void DiscFx()
    {
        audioPlayer.PlayOneShot(discFx, 0.25F / audioPlayer.volume);
    }

    /// <summary>
    /// Sets background music to intro or ambient
    /// </summary>
    /// <param name="intro"></param>
    public void BgMusic(bool intro)
    {
        if (intro)
        {
            audioPlayer.clip = introMusic;
            audioPlayer.loop = true;
            audioPlayer.volume = 0.05F;
        }
        else
        {
            audioPlayer.clip = ambientMusic;
            audioPlayer.loop = true;
            audioPlayer.volume = 0.7F;
        }
        audioPlayer.Play();
    }
}
