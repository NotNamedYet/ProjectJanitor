using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class CharSelController : MonoBehaviour {

    public Image right;
    public Image left;

    public CharacterSelector charSelector;

    bool isValidate;
    int i = 0;
    public CharSelection[] tabChar;

    // Use this for initialization
    void Start() {
        ShowDefaultChar();
        Debug.Log(i + " - " + tabChar.Length);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ValidateCharName()
    {
        System.Text.RegularExpressions.Regex charNameValidator = new Regex("[A-Za-az0-9]");
        isValidate = charNameValidator.IsMatch(charSelector.charName.text);
        //Regex.IsMatch(charName, "[A-Za-az0-9]");
    }

    public void ShowDefaultChar()
    {
        tabChar[i].gameObject.SetActive(true);
    }

    public void SelectRight()
    {
        tabChar[i].gameObject.SetActive(false);
        if (i < (tabChar.Length - 1))
        {
            i++;
        }
        else
        {
            i = 0;
        }
        ShowSelectedChar();
        Debug.Log(i + " - " + tabChar.Length);
    }

    public void SelectLeft()
    {
        tabChar[i].gameObject.SetActive(false);
        if (i > 0)
        {
            i--;
        }
        else
        {
            i = (tabChar.Length - 1);
        }
        ShowSelectedChar();
        Debug.Log(i + " - " + tabChar.Length);
    }


    public void ShowSelectedChar()
    {
        tabChar[i].gameObject.SetActive(true);
        charSelector.selectedMarine = tabChar[i].marines;
    }

    /*
        public void ShowSelectedChar(CharSelection character)
        {
            if (character.isSelected == true) character.gameObject.SetActive(true);
        }

        public void HideCharacter(CharSelection character)
        {
            if (character.isSelected == false) character.gameObject.SetActive(false);
        }
    */
}
