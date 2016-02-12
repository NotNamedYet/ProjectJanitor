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

    void OnEnable()
    {
        //PauseManager.OnPause += TogglePauseMenu;
        GameController.EnterPauseEvent += ShowPauseMenu;
        GameController.ExitPauseEvent += HidePauseMenu;
    }

    void OnDisable()
    {
        //PauseManager.OnPause -= TogglePauseMenu;
        GameController.EnterPauseEvent -= ShowPauseMenu;
        GameController.ExitPauseEvent -= HidePauseMenu;
    }

    void Awake()
    {
        HidePauseMenu();
        savePanel.SetActive(false);
        loadPanel.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
    }

    /*	
        void TogglePauseMenu()
        {
            Debug.Log("Enter to Toggle Pause Menu");
            if (!pauseMenu.activeInHierarchy) pauseMenu.SetActive(true);
            else pauseMenu.SetActive(false);
            //GameController.Controller.isInPause = !GameController.Controller.isInPause;
        }
    */

    void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
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
        GameController.ExitPause();
    }
    /*
        public void QuitPress()
        {
            GameController.ExitPause();
            GameController.LoadScene("scn_MainMenu");
        }
    */

    // Use this for initialization

}
