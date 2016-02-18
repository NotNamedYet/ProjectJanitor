using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlsContext : MonoBehaviour {

    public GameObject descKeyPanel;
    public GameObject descMousePanel;

    public GameObject mouseCore;
    public GameObject mouseLB;
    public GameObject mouseRB;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowContext(bool isMouse, GameObject button)
    {
        if (isMouse == true)
        {
            descMousePanel.SetActive(true);
            button.GetComponent<Image>().color = Color.red;
        }
        else descKeyPanel.SetActive(true);
    }

    public void HideContext(GameObject button)
    {
        if (descKeyPanel.activeInHierarchy) descKeyPanel.SetActive(false);
        else if (descMousePanel.activeInHierarchy)
        {
            descMousePanel.SetActive(false);
            button.GetComponent<Image>().color = new Color32(71, 71, 71, 255);
        }
    }
    
}
