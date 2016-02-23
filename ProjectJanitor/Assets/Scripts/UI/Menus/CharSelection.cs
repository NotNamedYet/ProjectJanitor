using UnityEngine;
using System.Collections;

public class CharSelection : MonoBehaviour {

    public CharSelController charSelControl;

    public bool isSelected;

	// Use this for initialization
	void Awake () {
        ShowCharacter();
    }

    // Update is called once per frame
    void Update () {

    }

    public void ShowCharacter()
    {
        charSelControl.ShowSelectedChar(this);
    }

    public void HideCharacter()
    {
        charSelControl.HideCharacter(this);
    }



}
