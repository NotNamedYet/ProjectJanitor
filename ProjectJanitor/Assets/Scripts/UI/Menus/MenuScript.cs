using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public Canvas quitMenu;
    public Button startGame;
    public Button exitGame;

    // Use this for initialization
    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startGame = startGame.GetComponent<Button>();
        exitGame = exitGame.GetComponent<Button>();
        quitMenu.enabled = false;
    }

    void Update()
    {
        EscapeTracker();
    }

    public void EscapeTracker()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (quitMenu.enabled == true)
            {
                NoButtonPress();
            }
            else
            {
                QuitGamePress();
            }
        }
    }

    public void QuitGamePress()
    {
        quitMenu.enabled = true;
        startGame.enabled = false;
        exitGame.enabled = false;
    }

    public void NoButtonPress()
    {
        quitMenu.enabled = false;
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
        SceneManager.LoadScene("scn_MainMenu");
    }

}
