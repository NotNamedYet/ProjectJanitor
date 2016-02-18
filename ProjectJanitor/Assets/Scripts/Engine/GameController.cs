using UnityEngine;
using GalacticJanitor.Game;
using GalacticJanitor.UI;
using UnityEngine.SceneManagement;

namespace GalacticJanitor.Engine
{
    public class GameController : MonoBehaviour
    {

        public static GameController Controller { get; private set; }

        private StageLoader _stageLoader;
        private PlayerController _player;
        private TopDownCamera _topDownCamera;

        private GameObject _projectileHolder;

        /// <summary>
        /// Return the gameobject that hold this GameController.
        /// </summary>
        public static GameObject asGameObject
        {
            get { return Controller.gameObject; }
        }

        /// <summary>
        /// true if the game is Paused.
        /// </summary>
        /// <returns></returns>
        public static bool IsPause()
        {
            return IsPauseGame;
        }

        /// <summary>
        /// Staticaly return the actual StageLoader for the current scene.
        /// </summary>
        public static StageLoader StageLoader
        {
            get { return Controller._stageLoader; }
            set { Controller._stageLoader = value; }
        }

        /// <summary>
        /// Return the player.
        /// </summary>
        public static PlayerController Player
        {
            get { return Controller._player; }
            set
            {
                Controller._player = value;
            }
        }

        /// <summary>
        /// Destroy the active player (Be carefull with this... Murder a Janitor is punish by the law... If you get caught...)
        /// </summary>
        public static void DestroyPlayer()
        {
            Controller._DestroyPlayer();
        }

        /// <summary>
        /// If the PlayerHUD exists in the current scene, show the "Activate..." notifier.
        /// </summary>
        /// <param name="value">ture to show, false to hide</param>
        public static void TogglePlayerInteractor(bool value)
        {
            if (Player)
            {
                PlayerHUD hud = Player.m_HUD;
                if (hud)
                {
                    hud.ToggleInteractor(value);
                }
            }
        }

        /// <summary>
        /// If the PlayerHUD exists in the current scene, display a message to the player.
        /// </summary>
        /// <param name="message">Messagr to send</param>
        /// <param name="color">color</param>
        /// <param name="duration">how long this message will stay active</param>
        public static void NotifyPlayer(string message, Color color, int duration)
        {
            if (Player)
            {
                PlayerHUD hud = Player.m_HUD;
                if (hud)
                {
                    hud.Notify(message, color, duration);
                }
            }
        }

        /// <summary>
        /// Return the topDownCamera of the current scene
        /// </summary>
        public static TopDownCamera TopDownCamera
        {
            get { return Controller._topDownCamera; }
            set { Controller._topDownCamera = value; }
        }

        /// <summary>
        /// Return the _Projectiles folder in hierachy (creates one if missing at runtime)
        /// </summary>
        public static Transform ProjectileHolder
        {
            get
            {
                if (Controller._projectileHolder == null)
                {
                    Controller._projectileHolder = new GameObject("_Projectiles");
                }
                return Controller._projectileHolder.transform;
            }
        }

        public bool isInPause = false;

        void Awake()
        {

            if (Controller == null)
            {
                DontDestroyOnLoad(transform.root.gameObject);
                Controller = this;

            }
            else if (Controller != this)
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                if (!IsPauseGame) EnterPause();
                else ExitPause();
            }
        }

        public void _DestroyPlayer()
        {
            if (Player)
            {
                Destroy(Player.gameObject);
            }
        }
        

        #region EVENTSMANAGER

        public delegate void PressPauseButton();
        public static event PressPauseButton EnterPauseEvent;
        public static event PressPauseButton ExitPauseEvent;

        public static bool IsPauseGame { get; private set; }

        public static void EnterPause()
        {
            if (!IsPauseGame)
            {
                Time.timeScale = 0;
                IsPauseGame = true;
                if(EnterPauseEvent != null)
                {
                    EnterPauseEvent();
                }
            }
        }

        public static void ExitPause()
        {
            if (IsPauseGame)
            {
                Time.timeScale = 1;
                IsPauseGame = false;
                if(ExitPauseEvent != null)
                {
                    ExitPauseEvent();
                }
            }
        }

        #endregion

        //EDITOR

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "ico_Tools01.png", true);
        }


        void OnGUI()
        {
            if (Player)
            {
                GUI.Label(new Rect(Screen.width - 210, 10, 200, 20), "-PlayerPos : " + Player.transform.position.ToString());
                GUI.Label(new Rect(Screen.width - 210, 30, 200, 20), "-Stage : " + SceneManager.GetActiveScene().name);
            }
        }
    } 
}
