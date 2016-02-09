using UnityEngine;
using System.Collections;
using System;

public class PersistentSpawner : MonoBehaviour {

    [Header("Don't Make Prefab of this")]

    [UniqueIdentifier]
    public string uniqueId;
    public PersistentObject spawnable;
    public bool canSpawn = true;

	// Use this for initialization
	void Start ()
    {
        SpawnObject();
	}

    void SpawnObject()
    {
        if (canSpawn) //if spawner globally allows spawning 
        {
            Vector3 spawnPos;
            Quaternion spawnRot;
            ObjectData data = SaveSystem.GetObjectData(uniqueId);

            if (data != null)
            {
                canSpawn = data.canSpawn;
                if (!canSpawn) return; //Kill process if object itself is not allowed to spawn...

                spawnPos = data.GetPosition();
                spawnRot = data.GetRotation();
            }
            else
            {
                spawnPos = transform.position;
                spawnRot = transform.rotation;
            }

            PersistentObject clone = Instantiate(spawnable, spawnPos, spawnRot) as PersistentObject;
            clone.UniqueId = uniqueId;
            clone.AssignObjectData(data);
        }
    }

   
}
