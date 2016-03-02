using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeButtonColor : MonoBehaviour {

    public Color disableImageColor;
    public Color enableImageColor;
    public Text displayText;
    public Color onOverColor;
    public Color defaultColor;
    public Color noInteractColor;

    Button contextButton;
    Image image;

    // Use this for initialization
    void Awake () {
        contextButton = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    void OnEnable()
    {
        if (!contextButton.interactable)
        {
            displayText.color = noInteractColor;
            if (image) image.color = disableImageColor;
        }
        else
        {
            displayText.color = defaultColor;
            if (image) image.color = enableImageColor;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ColorOnOver()
    {
        if (contextButton.interactable)
            displayText.color = onOverColor;
    }

    public void ResetColor()
    {
        if (contextButton.interactable)
            displayText.color = defaultColor;
    }
}
