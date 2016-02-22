using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {

    public Button backButton;

    public void OtherSceneBackButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
