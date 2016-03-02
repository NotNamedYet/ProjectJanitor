using UnityEngine;
using System.Collections;

public class TimeController {

    public bool GamePaused { get; private set; }
    public InterfaceController iController { get; set; }

    bool gameOver = false;
    bool pauseLocked = false;

    public void GameOver(bool value)
    {
        gameOver = value;

        LockPauseInteraction(false);

        Pause(value);

        if (iController)
        {
            iController.ToggleDeadScreen(value);
        }
        LockPauseInteraction(value);
    }

    public void PauseGame(bool value, bool showMenu)
    {
        if (!IsPauseBlocked(true) && !gameOver)
        {
            Pause(value);

            if (showMenu && iController)
                iController.TogglePauseScreen(GamePaused);
        }
    }

    public void LockPauseInteraction(bool value)
    {
        pauseLocked = value;
    }

    public bool IsPauseBlocked(bool log)
    {
        if (pauseLocked && log && !GamePaused)
        {
            Debug.Log("You can't pause now...");
        }
        return pauseLocked;
    }

    void Pause(bool value)
    {
        GamePaused = value;
        Time.timeScale = (GamePaused) ? 0 : 1;
    }

    public static implicit operator bool(TimeController value)
    {
        return value != null;
    } 
}
