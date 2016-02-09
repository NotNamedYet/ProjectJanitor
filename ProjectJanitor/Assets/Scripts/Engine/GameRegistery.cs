using UnityEngine;
using System.Collections.Generic;
using GalacticJanitor.Engine;

/// <summary>
/// Global Registery, serializable class, representation of a "Game Save"
/// that contains all persistent objects data by scene, Player's Data ...
/// </summary>
[System.Serializable]
public class GameRegistery
{
    public GameRegisterySnapshot snapshot;

    public PlayerData playerData;
    public Dictionary<int, SceneData> scenesData;
    
    public GameRegistery(string startingScene)
    {
        snapshot = new GameRegisterySnapshot();
        snapshot.activeScene = startingScene;
        scenesData = new Dictionary<int, SceneData>();
    }
}

/// <summary>
/// Snapshot of a GameRegistery that contains just some needed data to identify a registery
/// </summary>
[System.Serializable]
public class GameRegisterySnapshot
{
    public string activeScene;
    public float timePlayed;
}


/// <summary>
/// Holder class that contains all data of a scene, persistant objects, last player pos in that specific scene etc...
/// </summary>
[System.Serializable]
public class SceneData
{
    public int sceneIndex;

    public StageData stageData;
    public Dictionary<string, ObjectData> objectsData { get; private set; }

    public SceneData(int sceneIndex)
    {
        this.sceneIndex = sceneIndex;
        objectsData = new Dictionary<string, ObjectData>();
    }

}

[System.Serializable]
public class StageData
{
    public bool disallowPlayer;
    public SerializedVector3 playerPosition;
    public SerializedQuaternion playerRotation;

    public StageData(Vector3 playerPos, Quaternion playerRot, bool disallowPlayer)
    {
        playerPosition = Serializer.Serializevector3(playerPos);
        playerRotation = Serializer.SerializeQuaternion(playerRot);
        this.disallowPlayer = disallowPlayer;
    }

    public void TranslateLocation(out Vector3 pos, out Quaternion rot)
    {
        pos = Serializer.DeserializeVector3(playerPosition);
        rot = Serializer.DeserializeQuaternion(playerRotation);
    }
}

/// <summary>
/// Holder class that contains all data of a unique persistent object.
/// </summary>
[System.Serializable]
public abstract class ObjectData
{
    public bool canSpawn = true;
    public string UniqueId;
    public SerializedVector3 position;
    public SerializedQuaternion rotation;

    public void RegisterPosition(Vector3 position, Quaternion rotation)
    {
        this.position = Serializer.Serializevector3(position);
        this.rotation = Serializer.SerializeQuaternion(rotation);
    }

    public Vector3 GetPosition()
    {
        return Serializer.DeserializeVector3(position);
    }

    public Quaternion GetRotation()
    {
        return Serializer.DeserializeQuaternion(rotation);
    }
}
