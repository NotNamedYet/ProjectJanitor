using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using GalacticJanitor.Engine;

public class PauseMenu : MonoBehaviour {

    //public Canvas pauseMenu;

    public GameObject pauseMenu;
    public GameObject savePanel;
    public GameObject loadPanel;

    public Button resumeButton;
    public Button saveButton;
    public Button loadButton;
    //public Button cancelButton;
    public Button quitButton;

    public Animator pauseButtonsAnimator;

	// Use this for initialization
	void Start () {
        pauseMenu.SetActive(false);
        savePanel.SetActive(false);
        loadPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        Debug.Log("Enter to Toggle Pause Menu");
        if (!pauseMenu.activeInHierarchy) pauseMenu.SetActive(true);
        else pauseMenu.SetActive(false);
        GameController.Controller.isInPause = !GameController.Controller.isInPause;
        ToggleTimeScale();
    }
    
    void ToggleTimeScale()
    {
        if (pauseMenu.activeInHierarchy) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void SavePress()
    {
        int pos = -1;
        pauseButtonsAnimator.SetInteger("Position", pos);
        savePanel.SetActive(true);
        if (loadPanel.activeInHierarchy) loadPanel.SetActive(false);
    }

    public void LoadPress()
    {
        int pos = 1;
        pauseButtonsAnimator.SetInteger("Position", pos);
        loadPanel.SetActive(true);
        if (savePanel.activeInHierarchy) savePanel.SetActive(false);
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
