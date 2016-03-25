using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

public class PauseController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameController.PausedGame)
            {
                GameController.EnterPause();
            }
            else
            {
                GameController.ExitPause();
            }
        }
    }
}
