using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Selector : MonoBehaviour {

    public CharacterSelector charSel;
    public MarinesType marines;

    public Image cursorHighlight;
    public Selector other;

    public Text charName;

    public bool selected;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSelection()
    {
        other.Deselect();
        selected = true;
        cursorHighlight.gameObject.SetActive(true);
        cursorHighlight.GetComponent<Image>().color = Color.red;
        charSel.selectedMarine = marines;
        charSel.startButton.interactable = true;
        charName.text = marines.ToString();
        if (charName.text == "SgtHartman") charName.text = "Ricky Sofredo";
        else charName.text = "Clara Freegirl";
    }

    public void SelectedOn()
    {
        if (!selected)
        {
            cursorHighlight.gameObject.SetActive(true);
            cursorHighlight.GetComponent<Image>().color = Color.yellow;
        }
    }

    public void SelectedOff()
    {
        if (!selected)
        {
            cursorHighlight.gameObject.SetActive(false);
        }
    }

    public void Deselect()
    {
        selected = false;
        cursorHighlight.gameObject.SetActive(false);
    }

}
