using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour {

    public Text displayText;
    public Color onOverColor;
    public Color defaultColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ColorOnOver()
    {
        displayText.color = onOverColor;
    }

    public void ResetColor()
    {
        displayText.color = defaultColor;
    }
}
