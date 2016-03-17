using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using GalacticJanitor.Engine;
using MonoPersistency;

public class Map : MonoBehaviour {

    public GameObject mapCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mapCamera.activeInHierarchy)
                mapCamera.SetActive(false);
            else
                mapCamera.SetActive(true);
        }
    }
}
