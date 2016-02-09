using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

    //public AudioClip[] footsteps;
    public AudioClip footsteps;
    public AudioSource audioSource;


	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    void PlayFootsteps()
    {
        //audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)], 0.2f);
        audioSource.PlayOneShot(footsteps, 0.2f);
    }

}
