using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public ChangeTextColor controlsBB;
    public ChangeTextColor noQB;

    public GameObject quitMenu;
    public GameObject menuButtons;
    public GameObject commandsPanel;

    public Button startGame;
    public Button loadGame;
    public Button commandsButton;
    public Button exitGame;
    public Button backButton;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {
        EscapeTracker();
    }

    public void EscapeTracker()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (quitMenu.activeInHierarchy)
            {
                NoButtonPress();
            }
            else if (commandsPanel.activeInHierarchy)
            {
                commandsPanel.SetActive(false);
                menuButtons.SetActive(true);
            }
            else
            {
                QuitGamePress();
            }
        }
    }

    public void CommandsPress()
    {
        commandsPanel.SetActive(true);
        menuButtons.SetActive(false);
    }

    public void QuitGamePress()
    {
        quitMenu.SetActive(true);
        InteractableOff();
    }

    public void NoButtonPress()
    {
        noQB.OffTrigger();
        quitMenu.SetActive(false);
        InteractableOn();
    }

    public void NewGamePress()
    {
        //Application.LoadLevel("scn_CharSelection");
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        //Application.LoadLevel("scn_LoadMenu");
        SceneManager.LoadScene("scn_LoadGame");
    }

    public void YesButtonPress()
    {
        Application.Quit();
    }

    public void BackButtonPress()
    {
        if (commandsPanel.activeInHierarchy)
        {
            controlsBB.OffTrigger();
            commandsPanel.SetActive(false);
            menuButtons.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void InteractableOn()
    {
        startGame.interactable = true;
        loadGame.interactable = true;
        commandsButton.interactable = true;
        exitGame.interactable = true;
        menuButtons.GetComponent<AudioSource>().mute = false;
    }

    public void InteractableOff()
    {
        startGame.interactable = false;
        loadGame.interactable = false;
        commandsButton.interactable = false;
        exitGame.interactable = false;
        menuButtons.GetComponent<AudioSource>().mute = true;
    }

}
