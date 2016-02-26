using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class CharSelController : MonoBehaviour {

    public Image right;
    public Image left;
    public Image checkOK;
    public Image checkNOK;

    public CharacterSelector charSelector;

    bool isValidate;
    int i = 0;
    public CharSelection[] tabChar;

    // Use this for initialization
    void Start() {
        ShowDefaultChar();
        //StartCoroutine("CharNameValidate");
        //StartCoroutine("ShowOKCheck");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            name = charSelector.charName.text.Trim();
            Debug.Log(name);
        }
    }

    public void ValidateCharName()
    {
        System.Text.RegularExpressions.Regex charNameValidator = new Regex("[a-zA-Z]");
        isValidate = charNameValidator.IsMatch(charSelector.charName.text);
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
    }

    public void ShowSelectedChar()
    {
        tabChar[i].gameObject.SetActive(true);
        charSelector.selectedMarine = tabChar[i].marines;
    }

    public void DisplayOK(bool value)
    {
        checkOK.gameObject.SetActive(value);
    }

    public void DisplayNOK(bool value)
    {
        checkNOK.gameObject.SetActive(value);
    }

    IEnumerator CharNameValidate()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ValidateCharName();
        }
        yield return null;
    }

    IEnumerator ShowOKCheck()
    {
        DisplayOK(true);
        yield return new WaitForSeconds(2.0f);
        DisplayOK(false);
    }

    IEnumerator ShowNOKCheck()
    {
        DisplayNOK(true);
        yield return new WaitForSeconds(2.0f);
        DisplayNOK(false);
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
