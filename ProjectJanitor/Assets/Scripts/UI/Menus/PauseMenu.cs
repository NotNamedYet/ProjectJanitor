using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public Canvas pauseMenu;

    public Image savePanel;
    public Image loadPanel;

    public Button resumeButton;
    public Button saveButton;
    public Button loadButton;
    //public Button cancelButton;
    public Button quitButton;

    public Animator pauseButtonsAnimator;

    /* Test 
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    /* Test */

	// Use this for initialization
	void Start () {
        pauseMenu.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            savePanel.enabled = false;
            loadPanel.enabled = false;
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        pauseMenu.enabled = !pauseMenu.enabled;
        ToggleTimeScale();
    }

    void ToggleSavePanel()
    {
        savePanel.enabled = !savePanel.enabled;
    }

    void ToggleLoadPanel()
    {
        loadPanel.enabled = !loadPanel.enabled;
    }
    
    void ToggleTimeScale()
    {
        if (pauseMenu.enabled)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void TestLeft()
    {
        int pos = -1;
        pauseButtonsAnimator.SetInteger("Position", pos);
        ToggleSavePanel();
    }

    public void TestRight()
    {
        int pos = 1;
        pauseButtonsAnimator.SetInteger("Position", pos);
        ToggleLoadPanel();
    }

    public void ResumePress()
    {
        int pos = 0;
        pauseButtonsAnimator.SetInteger("Position", pos);
        TogglePauseMenu();
    }

    public void QuitPress()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("scn_MainMenu");
    }

}
