using UnityEngine;
using System.Collections.Generic;
using System;
using GalacticJanitor.Engine;
using UnityEngine.SceneManagement;
using GalacticJanitor.Game;

namespace GalacticJanitor.Persistency
{
    [Serializable]
    public class Registery
    {
        public RegisterySnapshot snapshot;

        public PlayerData playerData;
        public Dictionary<string, ObjectData> objectData;
        public Dictionary<string, SceneData> scenesData;

        public Registery(string startingScene)
        {
            snapshot = new RegisterySnapshot();
            snapshot.RefreshIdentifier();
            snapshot.lastScene = startingScene;

            objectData = new Dictionary<string, ObjectData>();
            scenesData = new Dictionary<string, SceneData>();
            
        }

    }

    [Serializable]
    public class RegisterySnapshot
    {
        public string identifier;
        public string playerName;
        public string lastScene;
        public long firstTimeCreated;

        public string formatTimePlayed;

        private TimeSpan timePlayed;
        

        private long lastTimeUpdate;

        public void RefreshIdentifier()
        {
            firstTimeCreated = DateTime.Now.ToBinary();
            identifier = "GameSave-" + firstTimeCreated;
        }

        public void UpdateTime()
        {
            lastTimeUpdate = DateTime.Now.ToBinary();
        }

        public void UpdateTimePlayed()
        {
            if (lastTimeUpdate == 0)
            {
                return;
            }
            DateTime last = DateTime.FromBinary(lastTimeUpdate);
            DateTime now = DateTime.Now;

            var played = now - last;

            Debug.Log("Played: " + played);

            timePlayed += played;

            Debug.Log("Playedafter: " + timePlayed);

            lastTimeUpdate = now.ToBinary();

            string h = timePlayed.Hours > 0 ? timePlayed.Hours + "h" : "";
            string m = timePlayed.Minutes > 0 ? timePlayed.Minutes + "m" : "";

            Debug.Log("h: " + h + " m: " + m);

            formatTimePlayed = h + " " + m;

        }
        
    }

    [Serializable]
    public class PlayerData
    {
        public string playerName;
        public MarinesType playerType;

        public int playerHealth;
        public int playerMaxHealth;
        public int playerArmor;
        public int playerMaxArmor;

        public int inventoryAmmo0;
        public int inventoryAmmo1;
        public int magazineAmmo0;
        public int magazineAmmo1;

        public int weaponIndex;
    }

    [Serializable]
    public class SceneData
    {
        public string sceneName;
        public bool discovered;
        public SerializedVector3 lastPlayerLocation;
        public SerializedQuaternion lastPlayerRotation;

        public SceneData(string sceneName)
        {
            this.sceneName = sceneName;

            lastPlayerLocation = new SerializedVector3();
            lastPlayerLocation.x = 0;
            lastPlayerLocation.y = 0;
            lastPlayerLocation.z = 0;

            lastPlayerRotation = new SerializedQuaternion();
            lastPlayerRotation.x = 0;
            lastPlayerRotation.y = 0;
            lastPlayerRotation.z = 0;
            lastPlayerRotation.w = 0;

            discovered = false;
        }
    }
}
