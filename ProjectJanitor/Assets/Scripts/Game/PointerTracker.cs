using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PointerTracker : MonoBehaviour {

    // Another beautiful comment...
    public static Vector3 MousePosition { get;  private set; }
    Camera mainCam;

	// Use this for initialization
	void Start () {
        mainCam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
