using UnityEngine;
using System.Collections;
using GalacticJanitor.Persistency;
using UnityEngine.SceneManagement;
using GalacticJanitor.Game;
using System.Collections.Generic;

namespace GalacticJanitor.Engine
{
    public class SceneDataLoader : MonoBehaviour
    {
        public PlayerController hartmanPrefab;
        public PlayerController carterPrefab;

        public bool spawnPlayerInScene;
        public MarinesType playerType;

        public Vector3 playerStartPos;
        public Vector3 playerStartRot;

        public PointerTracker camera;

        GameController controller;
        Scene scene;
        Vector3 lastPlayerPos;
        Quaternion lastPlayerRot;
        PlayerData playerData;
        

        void Awake()
        {
            scene = SceneManager.GetActiveScene();

            controller = FindObjectOfType<GameController>();
            controller.currentDataLoader = this;

            if (controller != null)
            { 
                LoadFromRegistery();
            }
        }

        // Use this for initialization
        void Start()
        {
            if (controller != null)
            {
                if (spawnPlayerInScene)
                {
                    if (controller.player == null)
                    {
                        SpawnPlayer();
                    }
                    else
                    {
                        controller.player.transform.position = lastPlayerPos;
                        controller.player.transform.rotation = lastPlayerRot;
                    }

                    if (camera)
                    {
                        camera.target = controller.player.transform;
                        camera.transform.position = controller.player.transform.position;
                    }

                }
                else
                {
                    if (controller.player != null)
                    {
                        //TODO : EXTERMINATE EXTERMINATE.... (the player... because we don't want him anymore..)
                    }
                }
            }
        }

        void SpawnPlayer()
        {
            PlayerController player;

            if (playerType == MarinesType.MajCarter)
            {
                player = Instantiate(carterPrefab, lastPlayerPos, lastPlayerRot) as PlayerController;
            }
            else
            {
                player = Instantiate(hartmanPrefab, lastPlayerPos, lastPlayerRot) as PlayerController;
            }

            player.transform.SetParent(controller.transform);
            GameController.Controller.player = player;
        }

        public void ChangeScene(int index)
        {
            RegisterScene();
            SceneManager.LoadScene(index);
        }

        void RegisterScene()
        {
            if (controller != null)
            {
                Registery reg = controller.Registery;

                if (reg != null)
                {
                    SceneData data = GetSceneData();

                    if (!reg.scenesData.ContainsKey(scene.name))
                    {
                        reg.scenesData.Add(scene.name, data);
                    }
                    else
                    {
                        reg.scenesData[scene.name] = data;
                    }
                }
            }
        }


        void LoadFromRegistery()
        {
            //SCENE DATA INIT
            SceneData sceneData;

            try
            {
                sceneData = controller.Registery.scenesData[scene.name];
                lastPlayerPos = Serializer.DeserializeVector3(sceneData.lastPlayerLocation);
                lastPlayerRot = Serializer.DeserializeQuaternion(sceneData.lastPlayerRotation);
            }
            catch(KeyNotFoundException)
            {
                sceneData = GetSceneData();
                lastPlayerPos = playerStartPos;
                lastPlayerRot = Quaternion.Euler(playerStartRot);
            }

            sceneData.discovered = true;


            //PLAYER INFOS INIT
            playerData = controller.Registery.playerData;

            if (playerData != null)
            {
                playerType = playerData.playerType;
            }
        }

        public SceneData GetSceneData()
        {
            PlayerController pc = GameController.Controller.player;
            if (pc != null)
            {
                lastPlayerPos = pc.transform.position;
                lastPlayerRot = pc.transform.rotation; 
            }

            SceneData data = new SceneData(scene.name);
            data.discovered = true;
            data.lastPlayerLocation = Serializer.Serializevector3(lastPlayerPos);
            data.lastPlayerRotation = Serializer.SerializeQuaternion(lastPlayerRot);

            return data;
        }
    } 
}
