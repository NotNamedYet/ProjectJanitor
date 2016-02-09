using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;

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

        void ReloadStage()
        {
            GameController.DestroyPlayer();
            LoadStage();
        }

        //load from registery info...
        void LoadStage()
        {
            SceneData dScene = SaveSystem.GetActiveSceneData();
            StageData dStage = dScene.stageData;

            if (dStage != null)
            {
                dStage.TranslateLocation(out playerPosition, out playerRotation);
                disallowPlayer = dStage.disallowPlayer;
            }
            else
            {
                dStage = new StageData(playerPosition, playerRotation, disallowPlayer);
                dScene.stageData = dStage;
            }

            PlayerData dPlayer = SaveSystem.GetPlayerData();

            if (dPlayer != null)
            {
                marines = dPlayer.marines;
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

            SaveSystem.GetActiveSceneData().stageData = new StageData(playerPosition, playerRotation, disallowPlayer);
        }

        void OnEnable()
        {
            SaveSystem.OnGameSaveEvent += RegisterStage;
            SaveSystem.OnGameLoadEvent += ReloadStage;
        }

        void OnDisable()
        {
            SaveSystem.OnGameSaveEvent -= RegisterStage;
            SaveSystem.OnGameLoadEvent -= ReloadStage;
        }
    } 
}
