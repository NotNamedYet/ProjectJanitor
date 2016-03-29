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
            LoadPlayerData();
            GameController.TopDownCamera.JumpToTarget();
        }

        //refresh after reload...
        void RefreshPlayer()
        {
            GameController.Player.transform.position = playerPosition;
            GameController.Player.transform.rotation = playerRotation;
            LoadPlayerData();
            GameController.TopDownCamera.JumpToTarget();
          
        }

        void LoadPlayerData()
        {
            GameController.Player.LoadData(SaveSystem.GetPlayerData());
        }

        //load from registery info...
        void LoadStage()
        {
            if (disallowPlayer && GameController.Player)
            {
                GameController.DestroyPlayer();
                return;
            }

            StageData dStage = SaveSystem.GetActiveSceneData().StageData;

            if (!dStage.NewEntry)
            {
                dStage.TranslateLocation(out playerPosition, out playerRotation);
                disallowPlayer = dStage.m_disallowPlayer;
            }
            else
            {
                dStage.RegisterPlayerLocation(playerPosition, playerRotation);
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

            StageData data = SaveSystem.GetActiveSceneData().StageData;
            data.RegisterPlayerLocation(playerPosition, playerRotation);
            data.m_disallowPlayer = disallowPlayer;
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
