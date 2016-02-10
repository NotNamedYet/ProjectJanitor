using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using GalacticJanitor.Game;

namespace GalacticJanitor.Engine
{
    public class GameController : MonoBehaviour
    {

        public static GameController Controller { get; private set; }

        private StageLoader _stageLoader;
        private PlayerController _player;
        private TopDownCamera _topDownCamera;

        private GameObject _entityHolder;
        private GameObject _projectileHolder;

        /// <summary>
        /// Return the gameobject that hold this GameController.
        /// </summary>
        public static GameObject asGameObject
        {
            get { return Controller.gameObject; }
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
            set { Controller._player = value; }
        }

        /// <summary>
        /// Return the topDownCamera of the actual scene
        /// </summary>
        public static TopDownCamera TopDownCamera
        {
            get { return Controller._topDownCamera; }
            set { Controller._topDownCamera = value; }
        }

        public static Transform EntityHolder
        {
            get
            {
                if (Controller._entityHolder == null)
                {
                    Controller._entityHolder = new GameObject("_Entities");
                }
                return Controller._entityHolder.transform;
            }
        }

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

        public static void DestroyPlayer()
        {
            Controller._DestroyPlayer();
        }

        public static bool IsPause()
        {
            return IsPauseGame;
        }

        
        public static void LoadScene(string scene)
        {
            SaveSystem.CallForUpdate();
            SceneManager.LoadScene(scene);
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
            Gizmos.DrawIcon(transform.position, "ico_Config.png", true);
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(Screen.width - 110, 10, 100, 20), "Save"))
            {
                SaveSystem.SaveParty();
            }
            if (GUI.Button(new Rect(Screen.width - 110, 40, 100, 20), "Load"))
            {
                SaveSystem.LoadParty();
            }
        }

    } 
}
