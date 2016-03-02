using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using GalacticJanitor.Engine;
using MonoPersistency;

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
    private int pos = 0;
    private int move = 0;

    void OnEnable()
    {
        //PauseManager.OnPause += TogglePauseMenu;
        //GameController.EnterPauseEvent += ShowPauseMenu;
        //GameController.ExitPauseEvent += HidePauseMenu;
    }

    void OnDisable()
    {
        //PauseManager.OnPause -= TogglePauseMenu;
        //GameController.EnterPauseEvent -= ShowPauseMenu;
        //GameController.ExitPauseEvent -= HidePauseMenu;
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

    public void TogglePauseMenu(bool value)
    {
        if (value) ShowPauseMenu();
        else HidePauseMenu();
    }

    void ShowPauseMenu()
    {
        saveButton.interactable = !SaveSystem.BlockedSave;
        pauseMenu.SetActive(true);
    }

    void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        savePanel.SetActive(false);
        loadPanel.SetActive(false);
        pos = 0;
        move = 0;
    }


    public void SavePress()
    {
        if(pos == 0)
        {
            pos = -1;
            pauseButtonsAnimator.SetInteger("Position", pos);
            //ShowSavePanel();
            //if (loadPanel.activeInHierarchy) HideLoadPanel();
        }
        if(pos == 1)
        {
            move = 1;
            pauseButtonsAnimator.SetInteger("Move", move);
            //ShowSavePanel();
            //if (loadPanel.activeInHierarchy) HideLoadPanel();
        }
        if(pos == -1 && move == -1)
        {
            pos = 1;
            move = 1;
            pauseButtonsAnimator.SetInteger("Position", pos);
            pauseButtonsAnimator.SetInteger("Move", move);
            //ShowSavePanel();
            //if (loadPanel.activeInHierarchy) HideLoadPanel();
        }
    }

    public void LoadPress()
    {
        if(pos == 0)
        {
            pos = 1;
            pauseButtonsAnimator.SetInteger("Position", pos);
            //ShowLoadPanel();
            //if (savePanel.activeInHierarchy) HideSavePanel();
        }
        if (pos == -1)
        {
            move = -1;
            pauseButtonsAnimator.SetInteger("Move", move);
            //ShowLoadPanel();
            //if (savePanel.activeInHierarchy) HideSavePanel();
        }
        if(pos == 1 && move == 1)
        {
            pos = -1;
            move = -1;
            pauseButtonsAnimator.SetInteger("Position", pos);
            pauseButtonsAnimator.SetInteger("Move", move);
            //ShowLoadPanel();
            //if (savePanel.activeInHierarchy) HideSavePanel();
        }
    }

    public void ResumePress()
    {
        pos = 0;
        move = 0;
        pauseButtonsAnimator.SetInteger("Position", pos);
        pauseButtonsAnimator.SetInteger("Move", move);
        if (savePanel.activeInHierarchy) HideSavePanel();
        if (loadPanel.activeInHierarchy) HideLoadPanel();
        GameController.ExitPause();
    }

    public void ShowSavePanel()
    {
        savePanel.SetActive(true);
    }

    public void HideSavePanel()
    {
        savePanel.SetActive(false);
    }

    public void ShowLoadPanel()
    {
        loadPanel.SetActive(true);
    }

    public void HideLoadPanel()
    {
        loadPanel.SetActive(false);
    }

    public void QuitPress()
    {
        GameController.ExitPause();
        SceneManager.LoadScene(0);
    }
}
