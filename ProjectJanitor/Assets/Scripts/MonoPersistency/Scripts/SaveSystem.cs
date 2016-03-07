using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System;
using GalacticJanitor.Engine;

namespace MonoPersistency
{
    public class SaveSystem : MonoBehaviour
    {
        public string m_startingLevel = "SampleLab";
        GameRegistery m_registery;

        #region SINGLETON
        public static SaveSystem instance { get; private set; }
        public static bool BlockedSave { get; set; }

        /// <summary>
        /// Return the current GameRegistery loaded
        /// </summary>
        public static GameRegistery Registery
        {
            get
            {
                if (instance.m_registery == null)
                {
                    instance.m_registery = new GameRegistery(instance.m_startingLevel);
                    Debug.Log("Registery Missing, new one created.");
                }
                return instance.m_registery;
            }
            private set
            {
                instance.m_registery = value;
            }
        }

        /// <summary>
        /// Singleton init
        /// </summary>
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                _StartPT();
                snaps = LoadSnapshots();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region EVENTS
        public delegate void EventSaver();

        /// <summary>
        /// Event fired when Registry is written to disk
        /// </summary>
        public static event EventSaver OnDiskSaveEvent;

        /// <summary>
        /// Event Fired when Registery is updated but not yet written to disk
        /// </summary>
        public static event EventSaver OnUpdateRegisteryEvent;

        /// <summary>
        /// Occures before loading a party
        /// </summary>
        public static event EventSaver OnPreLoadEvent;

        /// <summary>
        /// Occures right after loading a party
        /// </summary>
        public static event EventSaver OnPostLoadEvent;

        /// <summary>
        /// Event Caller : OnUpdateRegisteryEvent
        /// </summary>
        public static void UpdateRegistery()
        {
            if (OnUpdateRegisteryEvent != null)
                OnUpdateRegisteryEvent();
        }

        /// <summary>
        /// Update and write the current Registery to disk.
        /// Fire OnUpdateRegisteryEvent then OnDiskSaveEvent
        /// </summary>
        /// <param name="newSave">is this action requires a new file?</param>
        public static void SaveAndWrite(bool newSave)
        {
            UpdateRegistery();

            if (OnDiskSaveEvent != null)
                OnDiskSaveEvent();

            Registery.m_snapshot.m_currentScene = SceneManager.GetActiveScene().name;
            WriteRegistery(newSave);
        }

        /// <summary>
        /// Update and write the current Registery to disk.
        /// </summary>
        /// <param name="overriden">The snapshot file to override</param>
        public static void SaveAndWrite(RegisterySnapshot overriden)
        {
            Registery.m_snapshot.m_identifier = overriden.m_identifier;
            SaveAndWrite(false);
        }

        /// <summary>
        /// Load the regitery (and the party right after) 
        /// Fire OnPreLoadEvent before any actions, then OnPostLoadEvent at the very end  
        /// </summary>
        /// <param name="snap">The snapshot to load</param>
        public static void LoadGame(RegisterySnapshot snap)
        {
            if (OnPreLoadEvent != null)
                OnPreLoadEvent();

            PausePlayTimer(true);
            LoadRegistery(snap);


            if (OnPostLoadEvent != null)
                OnPostLoadEvent();

        }
        #endregion

        #region SAVING_LOADING
        /// <summary>
        /// Return all the Registery Snapshots stored under the saves directory
        /// </summary>
        /// <returns></returns>
        public static RegisterySnapshot[] LoadSnapshots()
        {
            BinaryFormatter serializer = new BinaryFormatter();
            List<RegisterySnapshot> snaps = new List<RegisterySnapshot>();
            string dirPath = Application.persistentDataPath + "/saves/";

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            foreach (string file in Directory.GetFiles(Application.persistentDataPath + "/saves/"))
            {
                if (file.EndsWith(".gjs"))
                {
                    FileStream stream = File.Open(file, FileMode.Open);
                    RegisterySnapshot s = serializer.Deserialize(stream) as RegisterySnapshot;
                    snaps.Add(s);
                    stream.Close();
                }
            }

            snaps.Sort();
            snaps.Reverse();

            return snaps.ToArray();
        }

        private static void WriteRegistery(bool newSave)
        {
            if (newSave) Registery.NewID();
            WriteRegistery();
        }

        private static void WriteRegistery()
        {
            Registery.m_snapshot.m_lastUpdate = DateTime.Now.ToBinary();
            Write(Registery.m_snapshot.m_identifier);
        }

        private static void Write(string path)
        {
            Debug.Log("writing");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = File.Create(Application.persistentDataPath + "/saves/" + path + ".gjd");
            bf.Serialize(stream, Registery);
            stream.Close();

            FileStream stream2 = File.Create(Application.persistentDataPath + "/saves/" + path + ".gjs");
            bf.Serialize(stream2, Registery.m_snapshot);
            stream2.Close();
        }

        private static void LoadRegistery(RegisterySnapshot snap)
        {
            string path = "/saves/" + snap.m_identifier + ".gjd";

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = File.Open(Application.persistentDataPath + path, FileMode.Open);

            Registery = bf.Deserialize(stream) as GameRegistery;
            stream.Close();

            PausePlayTimer(false);
            GameController.TimeController.GameOver(false);
            BlockedSave = false;
            SceneManager.LoadScene(Registery.m_snapshot.m_currentScene);
        }
        #endregion

        #region DATA_PROCESSING
        /// <summary>
        /// Register the DataContainer of the player
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterPlayer(DataContainer container)
        {
            Registery.m_player = container;
        }

        /// <summary>
        /// Check if the GameRegistery has a DataContainer for the player
        /// </summary>
        /// <param name="container">output the DataContainer if it exists or null</param>
        /// <returns></returns>
        public static bool HasPlayerData(out DataContainer container)
        {
            container = Registery.m_player;
            return container != null;
        }

        /// <summary>
        /// return the DataContainer of the player if it exists or null
        /// </summary>
        /// <returns></returns>
        public static DataContainer GetPlayerData()
        {
            return Registery.m_player;
        }

        /// <summary>
        /// Callback called by a MonoPersistentInitializer to load a MonoPersistent object
        /// </summary>
        /// <param name="uniqueName">the unique id of the MonoPersistent</param>
        /// <param name="obj">the related Monopersistent</param>
        public static void LoadMonoPersistent(string uniqueName, MonoPersistent obj)
        {
            DataContainer dCon;

            if (HasData(uniqueName, out dCon))
            {
                if (dCon.m_spawnable)
                {
                    obj.LoadData(dCon);
                }
                else
                {
                    Destroy(obj.gameObject);
                }
            }
        }

        /// <summary>
        /// Register a container into the SceneData of the active scene
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterData(DataContainer container)
        {
            string key = container.m_key;

            if (key.EndsWith(PrettyCode.SUFFIX))
            {
                SceneData sData = GetActiveSceneData();

                if (sData.m_datas.ContainsKey(key))
                {
                    sData.m_datas[key] = container;
                }
                else
                {
                    sData.m_datas.Add(key, container);
                }
            }
            else
            {
                Debug.LogWarning("Insertion Aborted. Incorrect Key Validation: " + key + " MonoPersistent probably needs to be baked.");
            }
        }

        /// <summary>
        /// Check if a DataContainer exists for the givenkey into the SceneData of the active scene and output the result
        /// </summary>
        /// <param name="key">The Id of the container</param>
        /// <param name="output">result if it exists or null</param>
        /// <returns>true if a DataContainer has been found</returns>
        public static bool HasData(string key, out DataContainer output)
        {
            SceneData sData = GetActiveSceneData();

            if (sData.m_datas.ContainsKey(key))
            {
                output = sData.m_datas[key];
                return true;
            }
            else
            {
                output = null;
                return false;
            }
        }

        /// <summary>
        /// Return the SceneData of the current loaded scene
        /// </summary>
        /// <returns></returns>
        public static SceneData GetActiveSceneData()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneData sData;

            if (Registery.m_scenes.ContainsKey(scene.buildIndex))
            {
                sData = Registery.m_scenes[scene.buildIndex];
            }
            else
            {
                sData = new SceneData(scene.buildIndex);
                Registery.m_scenes.Add(scene.buildIndex, sData);
            }

            return sData;
        }
        #endregion

        #region PLAYTIME_TIMER

        bool playTimerPaused;
        Coroutine playTimerRoutine;
        
        void _StartPT()
        {
            _StopPT();
            playTimerRoutine = StartCoroutine(PlayTimer());
        }

        void _StopPT()
        {
            if (playTimerRoutine != null)
            {
                StopCoroutine(playTimerRoutine);
            }
        }

        void _PausePT(bool value)
        {
            playTimerPaused = value;
        }

        IEnumerator PlayTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (!playTimerPaused) Registery.m_snapshot.m_timePlayed++;
            }
        }

        /// <summary>
        /// Start the PlayTimer coroutine
        /// </summary>
        public static void StartPlayTimer()
        {
            instance._StartPT();
        }

        /// <summary>
        /// Stop the PlayTimer coroutine
        /// </summary>
        public static void StopPlayTimer()
        {
            instance._StopPT();
        }

        /// <summary>
        /// Pause/Unpause the PlayTime incrementation (without interupting the coroutine)
        /// </summary>
        /// <param name="value">true for pausing</param>
        public static void PausePlayTimer(bool value)
        {
            instance._PausePT(value);
        }
        #endregion

        /// <summary>
        /// Load a scene after a GameSave update
        /// </summary>
        /// <param name="scene">Scene to load</param>
        public static void LoadScene(string scene)
        {
            UpdateRegistery();
            BlockedSave = false;
            GameController.TimeController.GameOver(false);

            if (GameController.Player.IsFighting)
                GameController.Player.StopCombatState();

            SceneManager.LoadScene(scene);
        }

        #region DEBUG_GUI

        RegisterySnapshot[] snaps;
        RegisterySnapshot selected;

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 20), "NewSave"))
            {
                SaveAndWrite(true);
                snaps = LoadSnapshots();
            }
            if (GUI.Button(new Rect(10, 40, 100, 20), "Save"))
            {
                SaveAndWrite(false);
                snaps = LoadSnapshots();
            }

            float pos = 70;
            int index = 1;

            if (snaps != null && snaps.Length > 0)
            {
                foreach (RegisterySnapshot rs in snaps)
                {
                    GUI.Label(new Rect(120, pos, 100, 20), "Sc:" + rs.m_currentScene);
                    GUI.Label(new Rect(230, pos, 100, 20), "Tp:" + rs.FormatTimePlayed);
                    if (GUI.Button(new Rect(10, pos, 100, 20), "save " + index))
                    {
                        LoadGame(rs);
                    }
                    pos += 30;
                    index++;
                }
            }

        }
        #endregion

    }
}
