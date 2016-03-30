using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButtonTextColorChange : MonoBehaviour {

    public Text text;

    public void OnTrigger()
    {
        text.color = new Color32(15,15,15,255);
    }

    public void OffTrigger()
    {
        text.color = new Color32(255,255,255,255);
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("Floor0_BadWakeUp");
    }

}
