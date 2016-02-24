using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharSelController : MonoBehaviour {

    public Image right;
    public Image left;

    public CharacterSelector charSelector;

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
        tabChar[i].gameObject.SetActive(true);
        charSelector.selectedMarine = tabChar[i].marines;

        Debug.Log(i + " - " + tabChar.Length);
    }

    public void SelectLeft()
    {
        tabChar[i].gameObject.SetActive(false);
        if (i > 0)
        {
            i--;
            tabChar[i].gameObject.SetActive(true);
            charSelector.selectedMarine = tabChar[i].marines;
        }
        else
        {
            i = (tabChar.Length - 1);
            tabChar[i].gameObject.SetActive(true);
            charSelector.selectedMarine = tabChar[i].marines;
        }
        Debug.Log(i + " - " + tabChar.Length);
    }

    public void RightChangeColor()
    {
        right.color = new Color32(43, 161, 233, 255);
    }

    public void LeftChangeColor()
    {
        left.color = new Color32(43, 161, 233, 255);
    }

    public void CancelRightColorChanges()
    {
        right.color = new Color32(71, 71, 71, 255);
    }

    public void CancelLeftColorChanges()
    {
        left.color = new Color32(71, 71, 71, 255);
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
