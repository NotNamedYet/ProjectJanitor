using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PixelScaling : MonoBehaviour {

    Camera cam;
    public float pixelToUnit = 28;

	// Use this for initialization
	void Start ()
    {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        cam.orthographicSize = (float)Math.Round(Screen.height / pixelToUnit / 2, 2);
    }
}
