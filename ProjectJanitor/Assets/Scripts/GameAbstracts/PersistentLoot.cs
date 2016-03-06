using UnityEngine;
using System.Collections;
using MonoPersistency;
using System;
using GalacticJanitor.Game;

public abstract class PersistentLoot : MonoPersistent
{

    protected bool PickedUp { get; set; }


    public override void CollectData(DataContainer container)
    {
        container.m_spawnable = !PickedUp;
    }

    public override void LoadData(DataContainer container){}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickedUp = OnLoot(other.GetComponent<PlayerController>());

            Save();

            if (PickedUp)
                Destroy(gameObject);

        }
    }

    /// <summary>
    /// Action executed on loot. Make this function return true if the loot is picked up and the action is performed
    /// </summary>
    /// <param name="player"></param>
    /// <returns>true if the loot is picked up and the action is performed</returns>
    protected abstract bool OnLoot(PlayerController player);

}
