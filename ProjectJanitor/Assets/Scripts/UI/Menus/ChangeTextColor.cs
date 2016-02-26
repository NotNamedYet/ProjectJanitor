using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour {

    public Text text;

    public void OnTrigger()
    {
        text.color = new Color32(43, 161, 233, 255);
    }

    public void OffTrigger()
    {
        text.color = new Color32(15, 15, 15, 255);
    }

}
