using UnityEngine;
using System.Collections;
using MonoPersistency;
using UnityEngine.UI;

public class PanelSaveHeader : MonoBehaviour {

    public Text partyText;
    public Text sceneText;
    public Text timePlayedText;

    internal void FillHeaderInfo(RegisterySnapshot snapshot)
    {
        partyText.text = snapshot.PartyName;
        sceneText.text = snapshot.m_currentScene;
        timePlayedText.text = snapshot.FormatTimePlayed;
    }

    internal void DisplayCustomInfo(string underMasterTitle, string underSceneText, string underTimeText)
    {
        if (string.IsNullOrEmpty(underMasterTitle))
            return;

        partyText.text = underMasterTitle;

        sceneText.text = (!string.IsNullOrEmpty(underSceneText)) ? underSceneText : "";
        timePlayedText.text = (!string.IsNullOrEmpty(underTimeText)) ? underTimeText : "";
    }

    internal void ResetHeaderDisplay()
    {
        partyText.text = "";
        sceneText.text = "";
        timePlayedText.text = "";
    }
}
