using UnityEngine;
using GalacticJanitor.Game;
using MonoPersistency;

namespace GalacticJanitor.Engine
{

    public class StageLoader : MonoBehaviour
    {

        public PlayerController hartman;
        public PlayerController carter;
        public bool disallowPlayer;
        public MarinesType marines; 

        public Vector3 playerPosition;
        Quaternion playerRotation;

        void Awake()
        {
            GameController.StageLoader = this;
            LoadStage();
        }

        //Spawn if no player.. 
        void SpawnPlayer()
        {
            PlayerController pClone;

            if (marines == MarinesType.MajCarter)
            {
                pClone = Instantiate(carter, playerPosition, playerRotation) as PlayerController;
            }
            else
            {
                pClone = Instantiate(hartman, playerPosition, playerRotation) as PlayerController;
            }

            pClone.transform.SetParent(GameController.asGameObject.transform);
            GameController.Player = pClone;
            GameController.TopDownCamera.JumpToTarget();
        }

        //refresh after reload...
        void RefreshPlayer()
        {
            GameController.Player.transform.position = playerPosition;
            GameController.Player.transform.rotation = playerRotation;
            GameController.TopDownCamera.JumpToTarget();
        }

        

        //load from registery info...
        void LoadStage()
        {
            SceneData dScene = SaveSystem.GetActiveSceneData();
            StageData dStage = dScene.m_stage;

            if (dStage != null)
            {
                dStage.TranslateLocation(out playerPosition, out playerRotation);
                disallowPlayer = dStage.m_disallowPlayer;
            }
            else
            {
                dStage = new StageData(playerPosition, playerRotation, disallowPlayer);
                dScene.m_stage = dStage;
            }

            DataContainer dPlayer = SaveSystem.GetPlayerData();

            if (dPlayer != null)
            {
                marines = dPlayer.GetValue<MarinesType>("marine");
            }

            if (!disallowPlayer)
            {
                if (GameController.Player)
                {
                    RefreshPlayer();
                }
                else
                {
                    SpawnPlayer();
                }
            }
        }

        void RegisterStage()
        {
            if (GameController.Player)
            {
                Transform pTransform = GameController.Player.transform;

                playerPosition = pTransform.position;
                playerRotation = pTransform.rotation;
            }

            SaveSystem.GetActiveSceneData().m_stage = new StageData(playerPosition, playerRotation, disallowPlayer);
        }


        //EVENT RELATED

        void OnEnable()
        {
            SaveSystem.OnUpdateRegisteryEvent += OnSave;
            SaveSystem.OnPreLoadEvent += OnPreLoad;
        }

        void OnDisable()
        {
            SaveSystem.OnUpdateRegisteryEvent -= OnSave;
            SaveSystem.OnPreLoadEvent -= OnPreLoad;
        }

        /// <summary>
        /// Everything that's need to occure when the PreLoadEvent is fired
        /// </summary>
        void OnPreLoad()
        {
            GameController.DestroyPlayer();
        }

        /// <summary>
        /// Everything that's need to occure when the OnSaveEvent is fired
        /// </summary>
        void OnSave()
        {
            RegisterStage();
        }
    } 
}
