using UnityEngine;
using System.Collections;

using GalacticJanitor.Engine;
using MonoPersistency;

namespace GalacticJanitor.Game
{
    public sealed class FocusTriggerConsole : MonoPersistent
    {

        [Header("FocusTriggerSettings")]
        public bool m_oneTimeOnly;
        [Tooltip("Target to see.")]
        public Transform m_focus;
        [Tooltip("Time to stay on the focus")]
        public int m_focusTime;
        public float focusScrollingSpeed;

        [Tooltip("Do you want add a notification to the player ?")]
        public bool m_notifyPlayer;
        public string m_notification;
        public Color m_notificationColor;

        [Header("Using Trigger GO")]
        public bool useTriggerGoScript;
        private TriggerGameObject trigger;

        private TopDownCamera m_camera;
        private PlayerController m_player;

        bool saveDestroy;
        bool onPlay;

        // Use this for initialization
        void Start()
        {
            m_camera = GameController.TopDownCamera;
            m_player = GameController.Player;
            m_notificationColor.a = 255;
            if (useTriggerGoScript)
            {
                trigger = GetComponent<TriggerGameObject>();
                if (trigger == null) Debug.Log("If you checked \"useTriggerGoScript\" you must add a TriggerGameObjectScript to this game object.");
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void CollectData(DataContainer container)
        {
            container.m_spawnable = !saveDestroy;
        }

        public override void LoadData(DataContainer container)
        {
            //Nothing to load.
        }

        public void LaunchFocus()
        {
            if (!onPlay)
            {
                GameController.TogglePlayerInteractor(false);
                StartCoroutine(CoRoutFocus());
            }
        }

        IEnumerator CoRoutFocus()
        {
            onPlay = true;
            m_player.freezed = true;
            m_player.invincible = true;

            m_camera.fixedTarget = false;
            m_camera.SetTarget(m_focus);
            m_camera.fixedTarget = true;
            m_camera.onScrolling = true;
            m_camera.scrollingSpeed = focusScrollingSpeed;

            while (m_camera.IsFarFromTarget())
            {
                yield return new WaitForSeconds(.2f);
            }

            yield return new WaitForSeconds(m_focusTime/2);
            OnFocus();
            yield return new WaitForSeconds(m_focusTime/2);

            m_camera.fixedTarget = false;
            m_camera.SetTarget(m_player.transform);
            m_camera.fixedTarget = true;

            while (m_camera.IsFarFromTarget())
            {
                yield return new WaitForSeconds(.2f);
            }

            m_player.freezed = false;
            m_player.invincible = false;

            onPlay = false;

            if (m_oneTimeOnly)
            {
                saveDestroy = true;
                Save();
            }
        }

        private void OnFocus()
        {
            Activator activator = GetComponent<Activator>();
            if (activator.m_actor is ActivableDoor)
            {
                (activator.m_actor as ActivableDoor).m_notify = false;
            }

            activator.m_actor.OnInteract();

            if (m_notification.Length > 0 && m_notifyPlayer) GameController.NotifyPlayer(m_notification, m_notificationColor, 2);

            if (useTriggerGoScript) trigger.ActiveGO();
        }

    } 
}
