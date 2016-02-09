using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveSystem : MonoBehaviour {

    public static bool CanSave { get; private set; }
    public static SaveSystem Instance { get; private set; }
    public GameRegistery registery { get; private set; }
    public string firstGameSceneName;


    // ++ [EVENTS]
    public delegate void GameSaver();
    public static event GameSaver OnGameSaveEvent;
    public static event GameSaver OnGameLoadEvent;

    /// <summary>
    /// Call the SaveSystem.OnGameSaveEvent
    /// </summary>
    public static void CallForUpdate()
    {
        if (CanSave && OnGameSaveEvent != null)
        {
            OnGameSaveEvent();
        }
    }

    /// <summary>
    /// Call the SaveSystem.OnGameLoadEvent
    /// </summary>
    public static void ReloadGame()
    {
        if (OnGameLoadEvent != null)
        {
            OnGameLoadEvent();
        }
    }
    // -- [EVENTS]

    public static GameObject asGameObject
    {
        get { return Instance.gameObject; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CanSave = true;
            DontDestroyOnLoad(gameObject);
            GetRegistery();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static GameRegistery GetRegistery()
    {
        if (Instance != null)
        {
            if (Instance.registery == null)
            {
                Debug.Log("Registery missing, new one created.");
                Instance.registery = new GameRegistery(Instance.firstGameSceneName);
            }
            return Instance.registery;
        }
        else
        {
            Debug.LogError("Save System Missing...");
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static SceneData GetActiveSceneData()
    {
        Scene active = SceneManager.GetActiveScene();

        SceneData data;

        try
        {
            data = GetRegistery().scenesData[active.buildIndex];
        }
        catch (KeyNotFoundException)
        {
            data = new SceneData(active.buildIndex);
            GetRegistery().scenesData.Add(data.sceneIndex, data);
        }

        return data;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static SceneData GetSceneData(int index)
    {
        GameRegistery reg = GetRegistery();

        if (reg.scenesData.ContainsKey(index))
            return reg.scenesData[index];

        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uniqueId"></param>
    /// <returns></returns>
    public static ObjectData GetObjectData(string uniqueId)
    {
        return GetObjectData(SceneManager.GetActiveScene().buildIndex, uniqueId); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <param name="uniqueId"></param>
    /// <returns></returns>
    public static ObjectData GetObjectData(int sceneIndex, string uniqueId)
    {
        SceneData sceneData = GetSceneData(sceneIndex);

        if (sceneData != null && sceneData.objectsData.ContainsKey(uniqueId))
        {
            return sceneData.objectsData[uniqueId];
        }

        return null;
    }

    /// <summary>
    /// Get the PlayerData attached to the current loaded registery
    /// </summary>
    /// <returns>null if not exists</returns>
    public static PlayerData GetPlayerData()
    {
        return GetRegistery().playerData;
    }

    /// <summary>
    /// Save an ObjectData to the current registery.
    /// </summary>
    /// <param name="data">ObjectData to save</param>
    public static void SaveObject(ObjectData data)
    {
        if (CanSave && !string.IsNullOrEmpty(data.UniqueId))
        {
            SceneData scene = GetActiveSceneData();

            if (scene.objectsData.ContainsKey(data.UniqueId))
                scene.objectsData[data.UniqueId] = data;
            else scene.objectsData.Add(data.UniqueId, data);
        }
    }

    /// <summary>
    /// Save the PlayerData to the current registery.
    /// </summary>
    /// <param name="data"></param>
    public static void SavePlayer(PlayerData data)
    {
        if (CanSave)
        {
            Instance.registery.playerData = data;
        }
    }

    /// <summary>
    /// Update, Save, and Write the current Registery.
    /// </summary>
    public static void SaveParty()
    {
        CallForUpdate();
        Instance.WriteRegistery();
    }

    /// <summary>
    /// Reload the game to the previous written state (in file)
    /// </summary>
    public static void LoadParty()
    {
        Instance.LoadRegistery();
        ReloadGame();
    }

    /// <summary>
    /// Disallow saving when application is quitting 
    /// </summary>
    public void OnApplicationQuit()
    {
        CanSave = false;
    }

    /// <summary>
    /// Call a GameRegisteryWriter to write serialize the state of the registery.
    /// </summary>
    void WriteRegistery()
    {
        if (CanSave)
        {
            registery.snapshot.activeScene = SceneManager.GetActiveScene().name;
            new GameRegisteryWriter(registery).WriteRegistery();
        }
    }

    /// <summary>
    /// Load all snapshots (.gjs) file from the save directory 
    /// </summary>
    /// <returns></returns>
    public GameRegisterySnapshot[] LoadAllSnapshots()
    {
        GameRegisteryLoader loader = new GameRegisteryLoader();
        GameRegisterySnapshot[] snaps = loader.LoadSnapshots();

        return snaps;
    }

    /// <summary>
    /// Load the registery from file
    /// </summary>
    void LoadRegistery()
    {
        GameRegisteryLoader loader = new GameRegisteryLoader();
        registery = loader.LoadRegistery("GameSave");
        SceneManager.LoadScene(registery.snapshot.activeScene);
    }

    /// <summary>
    /// Nah.. Ya kno... Just because we can. 
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "ico_Config.png", true);
    }


}

/// <summary>
/// Internal class used for deserialization of Registeries
/// </summary>
internal class GameRegisteryLoader
{
    BinaryFormatter serializer = new BinaryFormatter();

    internal GameRegistery LoadRegistery(string name)
    {
        string filepath = Application.persistentDataPath + "/" + name + ".gjd";
        GameRegistery retVal;

        if (File.Exists(filepath))
        {
            FileStream stream = File.Open(filepath, FileMode.Open);
            retVal = serializer.Deserialize(stream) as GameRegistery;
        }
        else
        {
            retVal = new GameRegistery(SaveSystem.Instance.firstGameSceneName);
        }

        return retVal;
    }

    internal GameRegisterySnapshot[] LoadSnapshots()
    {
        List<GameRegisterySnapshot> snaps = new List<GameRegisterySnapshot>(1);

        if (File.Exists(Application.persistentDataPath + "/GameSave.gjs"))
        {
            FileStream stream = File.Open(Application.persistentDataPath + "/GameSave.gjs", FileMode.Open);
            GameRegisterySnapshot s = serializer.Deserialize(stream) as GameRegisterySnapshot;
            snaps.Add(s);
            stream.Close();
        }
        Debug.Log(snaps.Count);

        return snaps.ToArray();
           
    }
}

/// <summary>
/// Internal class used for serialization of Registeries
/// </summary>
internal class GameRegisteryWriter
{
    GameRegistery registery;
    GameRegisterySnapshot snapshot;

    internal GameRegisteryWriter(GameRegistery registery)
    {
        this.registery = registery;
        snapshot = registery.snapshot;
    }

    internal void WriteRegistery()
    {
        WriteSnapshot();
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create(Application.persistentDataPath + "/GameSave.gjd");
        formatter.Serialize(stream, registery);
        stream.Close();
    }

    void WriteSnapshot()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create(Application.persistentDataPath + "/GameSave.gjs");
        formatter.Serialize(stream, snapshot);
        stream.Close();
    }
}
