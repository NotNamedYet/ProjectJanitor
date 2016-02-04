using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using GalacticJanitor.Persistency;
using UnityEngine.SceneManagement;
using GalacticJanitor.Game;

namespace GalacticJanitor.Engine
{
    public class GameController : MonoBehaviour
    {

        public static GameController Controller { get; private set; }
        public Registery Registery { get; private set; }

        [Tooltip("Capable of collecting all Savables in a scene")]
        public Ambassador ambassador;

        [Tooltip("New game starting scene")]
        public string startingScene;

        [HideInInspector]
        public PlayerController player;

        //[HideInInspector]
        public SceneDataLoader currentDataLoader;

        void Awake()
        {

            if (Controller == null)
            {
                DontDestroyOnLoad(transform.root.gameObject);
                Controller = this;

                if (Registery == null)
                {
                    Registery = new Registery(startingScene);
                    Registery.snapshot.UpdateTime();
                }

            }
            else if (Controller != this)
            {
                Destroy(gameObject);
            }
        }

        public RegisterySnapshot[] LoadAllSnapshots()
        {
            List<RegisterySnapshot> list = new List<RegisterySnapshot>();

            BinaryFormatter formatter = new BinaryFormatter();

            foreach (string file in Directory.GetFiles(Application.persistentDataPath))
            {
                if (file.EndsWith(".gjs"))
                {
                    FileStream stream = File.Open(file, FileMode.Open);
                    RegisterySnapshot snap = formatter.Deserialize(stream) as RegisterySnapshot;
                    if (snap != null)
                    {
                        list.Add(snap);
                    }
                    stream.Close();
                }
            }

            return list.ToArray();

        }

        public void SaveRegistery(bool newFile)
        {
            if (newFile)
            {
                Registery.snapshot.RefreshIdentifier();
            }
            Registery.snapshot.UpdateTimePlayed();
            WriteRegisteryToFile(Registery.snapshot);
        }

        public void SaveRegistery(RegisterySnapshot snapshot)
        {
            snapshot.UpdateTimePlayed();
            WriteRegisteryToFile(snapshot);
        }

        public void LoadGameFromSnapshot(RegisterySnapshot snap)
        {
            Registery = LoadRegisteryFromFile(snap);
            Registery.snapshot.UpdateTime();
            SceneManager.LoadScene(snap.lastScene);
        }


        private void WriteRegisteryToFile(RegisterySnapshot snapshot)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream snapFile = File.Create(Application.persistentDataPath + "/" + snapshot.identifier + ".gjs");
            formatter.Serialize(snapFile, snapshot);
            snapFile.Close();

            FileStream dataFile = File.Create(Application.persistentDataPath + "/" + snapshot.identifier + ".gjd");
            formatter.Serialize(dataFile, Registery);
            dataFile.Close();
        }

        private Registery LoadRegisteryFromFile(RegisterySnapshot snapshot)
        {
            string path = Application.persistentDataPath + "/" + snapshot.identifier + ".gjd";

            Registery registery = null;

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream dataFile = File.Open(path, FileMode.Open);

                try
                {
                    registery = (Registery)formatter.Deserialize(dataFile);
                    return registery;
                }
                catch (Exception e)
                {
                    Debug.Log(e.StackTrace);
                    return CreateRegisteryFromSnapshot(snapshot);
                }
                finally
                {
                    dataFile.Close();
                }
            }
            else
            {
                return CreateRegisteryFromSnapshot(snapshot);
            }
        }

        private Registery CreateRegisteryFromSnapshot(RegisterySnapshot snap)
        {
            Registery registery = new Registery(startingScene);
            registery.snapshot = snap;
            return registery;
        }



    } 
}
