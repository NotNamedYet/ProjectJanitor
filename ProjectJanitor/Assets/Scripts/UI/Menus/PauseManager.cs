using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PauseManager : MonoBehaviour {

    public delegate void PressPauseButton();
    public static event PressPauseButton OnPause;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Pause();
        }
    }

    public static void Pause()
    {
        if (OnPause != null) OnPause();
    }
	
}
