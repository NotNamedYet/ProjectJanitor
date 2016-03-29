using UnityEngine;
using System.Collections;
using System;
using GalacticJanitor.Game;
using GalacticJanitor.Engine;

public class BossRoomFocus : FocusTrigger {

    public GameObject m_HUD;
    public AlienBoss boss;

    protected override void OnFocus()
    {
        m_HUD.SetActive(true);
        boss.rigging.Play("anm_AlienBoss_SpecialTaunt2");
        GameController.SceneSounds.PlayMusic(0);
    }
}
