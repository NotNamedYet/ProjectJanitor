using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PointerTracker : MonoBehaviour {

    // Real time updated mouse cursor position.
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
