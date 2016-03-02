using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

public class InterfaceController : MonoBehaviour {

    public GameObject headsUpDislpay;
    public PauseMenu pauseScreen;
    public GameObject deadScreen;

    public bool ScreenLock { get; set; }

    void Awake()
    {
        GameController.TimeController.iController = this;
    }

    public void ToggleDeadScreen(bool value)
    {
        ScreenLock = false;

        ToggleHUD(!value);
        deadScreen.SetActive(value);
        ScreenLock = value;
    }

    public void ToggleHUD(bool value)
    {
        if (!ScreenLock && headsUpDislpay)
            headsUpDislpay.SetActive(value);
    }

    public void TogglePauseScreen(bool value)
    {
        if (!ScreenLock && pauseScreen)
        {
            ToggleHUD(!value);
            pauseScreen.TogglePauseMenu(value);
        }
    }
}
