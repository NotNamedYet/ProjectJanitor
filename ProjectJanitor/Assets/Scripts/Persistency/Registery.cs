using UnityEngine;
using System.Collections.Generic;
using System;

namespace GalacticJanitor.Persistency
{
    [Serializable]
    public class Registery
    {
        public RegisterySnapshot snapshot;

        public PlayerData playerData;
        public Dictionary<string, ObjectData> objectData;

        public Registery(string startingScene)
        {
            snapshot = new RegisterySnapshot();
            snapshot.RefreshIdentifier();
            snapshot.lastScene = startingScene;

            objectData = new Dictionary<string, ObjectData>();
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
    public struct PlayerData
    {
        public string playerName;
        public float playerHealth;
    } 
}
