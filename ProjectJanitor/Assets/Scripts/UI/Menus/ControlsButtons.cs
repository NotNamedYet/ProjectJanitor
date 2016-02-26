using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlsButtons : MonoBehaviour {

    public ControlsContext contextPanel;

    public GameObject button;
    public GameObject link;
    public Text descText;

    public bool isMouseContext;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void Show()
    {
        link.SetActive(true);
        descText.gameObject.SetActive(true);
        contextPanel.ShowContext(isMouseContext, button);
    }

    public void Hide()
    {
        link.SetActive(false);
        descText.gameObject.SetActive(false);
        contextPanel.HideContext(button);
    }
}
