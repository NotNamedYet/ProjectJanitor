using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContextButton : MonoBehaviour {

    public Text contextButton;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InContextButton()
    {
        contextButton.color = new Color32(43, 161, 233, 255);
    }

    public void OutContextButton()
    {
        contextButton.color = new Color32(15, 15, 15, 255);
    }

}
