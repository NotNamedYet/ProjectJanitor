using UnityEngine;
using System.Collections;
using MonoPersistency;
using System;
using GalacticJanitor.Game;
using GalacticJanitor.Engine;

[RequireComponent(typeof(Collider))]
public abstract class FocusTrigger : MonoPersistent {

    public bool m_oneTimeOnly;
    public Transform m_focus;
    public int m_focusTime;

    public bool m_notifyPlayer;
    public string m_notification;
    public Color m_notificationColor;

    TopDownCamera m_camera;
    PlayerController m_player;

    bool saveDestroy;
    bool onPlay;

    // Use this for initialization
    protected virtual void Start ()
    {
        m_camera = GameController.TopDownCamera;
        m_player = GameController.Player;
	}

    public override void CollectData(DataContainer container)
    {
        container.m_spawnable = !saveDestroy;
    }

    public override void LoadData(DataContainer container)
    {
        //Nothing to load.
    }

    IEnumerator Focus()
    {
        onPlay = true;
        m_player.freezed = true;
        m_player.invincible = true;

        m_camera.fixedTarget = false;
        m_camera.SetTarget(m_focus);
        m_camera.fixedTarget = true;

        while (m_camera.IsFarFromTarget())
        {
            yield return new WaitForSeconds(.2f);
        }

        if (m_notification.Length > 0)
            GameController.NotifyPlayer(m_notification, m_notificationColor, 2);

        OnFocus();

        yield return new WaitForSeconds(m_focusTime);

        m_camera.fixedTarget = false;
        m_camera.SetTarget(m_player.transform);
        m_camera.JumpToTarget();

        m_player.freezed = false;
        m_player.invincible = false;

        onPlay = false;

        if (m_oneTimeOnly)
        {
            saveDestroy = true;
            Save();
        }

        Destroy(gameObject);
        
    }

    protected abstract void OnFocus();

    void OnTriggerEnter(Collider other)
    {
        if (!onPlay)
        {
            if (other.CompareTag("Player"))
                StartCoroutine(Focus());
        }
    }
}
