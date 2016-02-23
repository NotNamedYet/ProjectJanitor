using UnityEngine;
using System.Collections;

public class CharSelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowSelectedChar(CharSelection character)
    {
        Debug.Log(character.isSelected + " " + character.gameObject.name);
        if (character.isSelected == true) character.gameObject.SetActive(true);
    }

    public void HideCharacter(CharSelection character)
    {
        if (character.isSelected == false) character.gameObject.SetActive(false);
    }

}
