using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

public class EndGameActor : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameController.NotifyPlayer("Mission Acomplished !", Color.red, 6);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
