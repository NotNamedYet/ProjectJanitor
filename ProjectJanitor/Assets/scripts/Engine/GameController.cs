using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace GalacticJanitor.Engine
{
    public class GameController : MonoBehaviour
    {

        private static GameController instance;
        public static GameController Instance { get; private set; }

        public Player player;

        void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
                player = Instantiate(player);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private string ComposeSaveName()
        {
            long time = DateTime.Now.ToBinary();
            string scene = Application.loadedLevelName;

            return player.charName + "-" + scene + "-" + time;
        }

        public void SaveNewGame()
        {
            
            GameData data = new GameData();

            data.currentSceneName = Application.loadedLevelName;
            data.createdTime = DateTime.Now.ToBinary();

            data.playerHealth = player.health;
            data.playerName = player.charName;

            Transform playerPos = player.GetComponent<Transform>();

            data.playerPos = Serializer.Serializevector3(playerPos.position);
            data.playerRot = Serializer.SerializeQuaternion(playerPos.rotation);

            data.fileName = "/save-" + data.playerName + "-" + data.createdTime + ".gjs";

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + data.fileName);

            formatter.Serialize(file, data);
            file.Close();
        }

        public void OverrideSave(GameData data)
        {
            data.currentSceneName = Application.loadedLevelName;
            data.createdTime = DateTime.Now.ToBinary();

            data.playerHealth = player.health;
            data.playerName = player.charName;

            Transform playerPos = player.GetComponent<Transform>();

            data.playerPos = Serializer.Serializevector3(playerPos.position);
            data.playerRot = Serializer.SerializeQuaternion(playerPos.rotation);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + data.fileName);

            formatter.Serialize(file, data);
            file.Close();
        }

        public void LoadGame(string name)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(name, FileMode.Open);

            GameData data = (GameData)formatter.Deserialize(file);
            file.Close();

            LoadData(data);
            player.enabled = true;
        }

        public void LoadData(GameData data)
        {
            player.health = data.playerHealth;
            player.charName = data.playerName;
            player.GetComponent<Transform>().position = Serializer.DeserializeVector3(data.playerPos);
            player.GetComponent<Transform>().rotation = Serializer.DeserializeQuaternion(data.playerRot);
        }


        public void DeleteSave(GameData data)
        {
            File.Delete(Application.persistentDataPath + data.fileName);
        }

        public string[] ListSaves()
        {
            return Directory.GetFiles(Application.persistentDataPath);
        }


        public GameData[] ListGameDatas()
        {
            string[] files = ListSaves();
            List<GameData> datas = new List<GameData>();

            BinaryFormatter formatter = new BinaryFormatter();

            foreach (string path in files)
            {
                FileStream file = File.Open(path, FileMode.Open);
                GameData data = (GameData)formatter.Deserialize(file);
                datas.Add(data);
                file.Close();
            }

            return datas.ToArray();
        }


        // GUI
        // ____


        private GameData selected;
        
        void OnGUI()
        {
            GUI.Label(new Rect(10, 5, 100, 20), "Name : " + player.charName);
            GUI.Label(new Rect(10, 30, 200, 20), "Health : " + player.health);
            GUI.Label(new Rect(10, 55, 200, 20), "Pos : X:" + player.GetComponent<Transform>().position.x + " Y:" + player.GetComponent<Transform>().position.y + " Z:" + player.GetComponent<Transform>().position.z);

            if (GUI.Button(new Rect(150+30, 80, 150, 20), "Hit"))
            {
                player.health -= 1;
                if (player.health <= 0)
                    player.health = 0;
            }
            if (GUI.Button(new Rect(10, 80, 150, 20), "Health up"))
            {
                player.health += 10;
                if (player.health >= 100)
                    player.health = 100;
            }

            if (GUI.Button(new Rect(10, 120, 150, 20), "New Save"))
            {
                if (player.charName != null || player.charName.Length > 0)
                {
                    SaveNewGame();
                }
                else
                {
                    Debug.Log("Player Not found...");
                }
            }


            GameData[] datas = ListGameDatas();

            float pos = 220;

            foreach (GameData data in datas)
            {
                if (GUI.Button(new Rect(10, pos, 150, 20), data.playerName))
                {
                    selected = data;
                }
                pos += 25;
            }

            
            if (selected.playerName != null)
            {
                GUI.Label(new Rect(10, 180, 200, 20), "Party Selected : " + selected.playerName);

                DateTime time = DateTime.FromBinary(selected.createdTime);

                GUI.Label(new Rect(200, 220, 300, 20), "Date created :" + time);
                GUI.Label(new Rect(200, 240, 150, 20), "Health : " + selected.playerHealth);
                GUI.Label(new Rect(200, 260, 200, 20), "Pos : X:" + selected.playerPos.x + " Y:" + selected.playerPos.y + " Z:" + selected.playerPos.z);
                GUI.Label(new Rect(200, 280, 150, 20), "Location :" + selected.currentSceneName);

                if (GUI.Button(new Rect(10, pos + 30, 150, 20), "Load"))
                {
                    LoadData(selected);
                }
                if (GUI.Button(new Rect(10, pos + 60, 150, 20), "Save"))
                {
                    if (player.charName != null || player.charName.Length > 0)
                    {
                        OverrideSave(selected);
                    }
                    else
                    {
                        Debug.Log("Player Not found...");
                    }
                }
                if (GUI.Button(new Rect(10, pos + 90, 150, 20), "Delete"))
                {
                    DeleteSave(selected);
                }

            }
            else
            {
                GUI.Label(new Rect(10, 180, 200, 20), "Select a party...");
            }

           
        }

    }
}
