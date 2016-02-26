using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MonoPersistency;
using System.Text.RegularExpressions;

public class CharacterSelector : MonoBehaviour {

    public Button startButton;
    public MarinesType selectedMarine;

    public InputField charName;

    public Image checkNOK;
    public Image checkOK;

	// Use this for initialization
	void Start () {
                	   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnChangeCharName()
    {
        if(charName.text != "" && charName.text != " ")
        {
            startButton.interactable = true;
            StartCoroutine("DisplayCheckOK");
        }
        else
        {
            startButton.interactable = false;
            StartCoroutine("DisplayCheckNOK");
        }
    }

    public void LaunchGame()
    {
        SaveSystem.Registery.m_snapshot.m_partyName = charName.text;
        DataContainer playerContainer = new DataContainer("player");
        playerContainer.Addvalue("marine", selectedMarine);
        //...
        SaveSystem.RegisterPlayer(playerContainer);
        SaveSystem.LoadScene("SampleLab");
    }

    IEnumerator DisplayCheckOK()
    {
        checkOK.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        checkOK.gameObject.SetActive(false);
    }

    IEnumerator DisplayCheckNOK()
    {
        checkNOK.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        checkNOK.gameObject.SetActive(false);
    }
}
