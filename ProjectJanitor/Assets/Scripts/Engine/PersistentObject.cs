using UnityEngine;
using System.Collections;
using System;
using GalacticJanitor.Engine;

public abstract class PersistentObject : MonoBehaviour
{
    protected ObjectData objectData;

    [HideInInspector]
    public string UniqueId;
    public bool IsPersistent = true;
    public bool dynamicPersistency;

    public abstract ObjectData CreateData();
    public abstract void LoadData();

    /// <summary>
    /// Override this in a PersistantObject child object if you need to change the way an oject save itself in the Registery.
    /// (BuildData() should be called from here before any save in registery).
    /// </summary>
    public virtual void SaveObject()
    {
        if (IsPersistent)
        {
            BuildData();
            SaveSystem.SaveObject(objectData);
            Debug.Log("Saved " + gameObject.name + " can spawn : " + objectData.canSpawn);
        }
    }

    /// <summary>
    /// Override this in a PersistantObject child if you need to perform action when the SaveSystem load a party
    /// Nothing is executed by default.
    /// </summary>
    public virtual void OnGameLoad() {}

    //Event subscribe;
    protected virtual void OnEnable()
    {
        if (IsPersistent) SaveSystem.OnGameSaveEvent += SaveObject;
        if (IsPersistent) SaveSystem.OnGameLoadEvent += OnGameLoad;
    }

    //Event unsubscribe;
    protected virtual void OnDisable()
    {
        if (IsPersistent) SaveSystem.OnGameSaveEvent -= SaveObject;
        if (IsPersistent) SaveSystem.OnGameLoadEvent -= OnGameLoad;
    }

    /// <summary>
    /// Use to override Object data from outside.
    /// Use if you know what you do.
    /// </summary>
    /// <param name="data"></param>
    public void AssignObjectData(ObjectData data)
    {
        if (data == null)
        {
            data = CreateData();
        }
        objectData = data;
    }

    /// <summary>
    /// Build/Rebuild the ObjectData of this PersistentObject
    /// Should be called before any save of this object
    /// </summary>
    protected void BuildData()
    {
        objectData = CreateData();
    }
    

}


