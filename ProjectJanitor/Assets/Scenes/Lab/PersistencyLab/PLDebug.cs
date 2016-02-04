using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;
using GalacticJanitor.Persistency;
using UnityEngine.SceneManagement;

public class PLDebug : MonoBehaviour {

    public GameObject obj;

    void Awake()
    {
        Debug.Log("I cry from awake...");
    }

	void Start () {
        Debug.Log("I cry from start...");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void DBG01()
    {
        
    }

    int sc = 0;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 25), "go..."))
        {
            SceneManager.LoadScene(sc);
            if (sc == 0) sc = 2;
            else sc = 0;
        }
        if (GUI.Button(new Rect(10, 30, 100, 25), "toggle"))
        {
            if (obj) obj.SetActive(!obj.activeInHierarchy);
        }
    }
}
