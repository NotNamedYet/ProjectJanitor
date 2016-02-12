using UnityEngine;
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
            set
            {
                Controller._player = value;
            }
        }

        /// <summary>
        /// Return the topDownCamera of the actual scene
        /// </summary>
        public static TopDownCamera TopDownCamera
        {
            get { return Controller._topDownCamera; }
            set { Controller._topDownCamera = value; }
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
            return Controller.isInPause;
        }

        //EDITOR

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "ico_Config.png", true);
        }

    } 
}
