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

}
