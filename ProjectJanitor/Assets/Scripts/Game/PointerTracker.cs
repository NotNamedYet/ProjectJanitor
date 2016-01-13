using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PointerTracker : MonoBehaviour {

    public int jeViensFoutreLaMErdeDansLeScriptDeDede = 100; // Parce que 100 c'est cool
    public static Vector3 MousePosition { get;  private set; }
    Camera mainCam;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
