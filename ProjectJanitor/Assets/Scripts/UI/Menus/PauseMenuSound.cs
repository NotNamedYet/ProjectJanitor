using UnityEngine;
using System.Collections;

public class PauseMenuSound : MonoBehaviour {

    //public AudioClip[] footsteps;
    public AudioClip pausePanelSlide;
    public AudioSource audioSource;


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void SlidePanel()
    {
        //audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)], 0.2f);
        audioSource.PlayOneShot(pausePanelSlide, 0.2f);
    }
}
