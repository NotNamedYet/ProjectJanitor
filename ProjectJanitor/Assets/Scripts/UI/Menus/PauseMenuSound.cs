using UnityEngine;
using System.Collections;

public class PauseMenuSound : MonoBehaviour {

    public AudioClip[] pausePanelSlide;
    //public AudioClip pausePanelSlide;
    public AudioSource audioSource;

    public PauseMenu pauseMenuScript;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void SlidePanel()
    {
        //audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)], 0.2f);
        audioSource.PlayOneShot(pausePanelSlide[0], 0.2f);
    }

    void LongSlidePanel()
    {
        audioSource.PlayOneShot(pausePanelSlide[1], 0.2f);
    }

    void ShowSavePanel()
    {
        pauseMenuScript.ShowSavePanel();
    }

    void HideSavePanel()
    {
        pauseMenuScript.HideSavePanel();
    }

    void ShowLoadPanel()
    {
        pauseMenuScript.ShowLoadPanel();
    }

    void HideLoadPanel()
    {
        pauseMenuScript.HideLoadPanel();
    }
}
