using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public GameObject quitMenu;
    public GameObject commandsPanel;

    public Button startGame;
    public Button exitGame;
    public Button commandsButton;
    public Button backButton;

    // Use this for initialization
    void Start()
    {
        commandsPanel.SetActive(false);
        quitMenu.SetActive(false);
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
    }

    public void QuitGamePress()
    {
        quitMenu.SetActive(true);
        startGame.enabled = false;
        exitGame.enabled = false;
    }

    public void NoButtonPress()
    {
        quitMenu.SetActive(false);
        startGame.enabled = true;
        exitGame.enabled = true;
    }

    public void NewGamePress()
    {
        //Application.LoadLevel("scn_CharSelection");
        SceneManager.LoadScene("scn_CharSelection");
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
            commandsPanel.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene("scn_MainMenu");
        }
    }

}
