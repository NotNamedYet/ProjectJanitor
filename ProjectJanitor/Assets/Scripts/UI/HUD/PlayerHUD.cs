using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;
using UnityEngine.UI;

namespace GalacticJanitor.UI
{
    public class PlayerHUD : MonoBehaviour
    {

        public PlayerStateDisplay display;
        public GameObject m_intercator;
        public GameObject m_notifierPanel;
        public Text m_notifierText;

        private Coroutine noticeRoutine;

        // Use this for initialization
        void Awake()
        {
            GameController.Player.m_HUD = this;
            if (display)
            {
                GameController.Player.entityDisplay = display;
                GameController.Player.playerDisplay = display;
                GameController.Player.UpdateDisplay();
                GameController.Player.RefreshAmmoHUD();
            }
        }

        public void ToggleInteractor(bool value)
        {
            m_intercator.SetActive(value);
        }

        public void Notify(string message, Color color, int duration)
        {
            if (noticeRoutine != null) StopCoroutine(noticeRoutine);

            m_notifierText.color = color;
            m_notifierText.text = message.ToLower();

            noticeRoutine = StartCoroutine(NoticeRoutine(duration));
        }

        void DisplayNotice(bool value)
        {
            m_notifierPanel.SetActive(value);
        }

        IEnumerator NoticeRoutine(int sec)
        {
            DisplayNotice(true);
            yield return new WaitForSeconds(sec);
            DisplayNotice(false);
        }
    } 
}
