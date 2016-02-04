using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GalacticJanitor.Engine;

public class ObjDBG : MonoBehaviour {

    int index = 0;

	void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 20), "switch"))
        {
            GameController.Controller.currentDataLoader.ChangeScene(index);
            if (index == 0)
                index = 2;
            else index = 0;
        }
    }
}
