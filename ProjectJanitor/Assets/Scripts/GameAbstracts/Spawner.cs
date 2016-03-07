using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;
using System.Collections.Generic;
using MonoPersistency;
using System;
using GalacticJanitor.Engine;

public abstract class Spawner : MonoPersistent
{

    public AlienBase m_Alien;
    public Transform m_SpawnLocation;
    
    protected bool m_Active = true;

    protected AlienBase CreateAlien(bool active)
    {
        if (m_SpawnLocation == null) m_SpawnLocation = transform;

        AlienBase clone = Instantiate(m_Alien, m_SpawnLocation.position, m_SpawnLocation.rotation) as AlienBase;
        clone.DisablePersistency();
        clone.gameObject.SetActive(active);
        clone.transform.SetParent(GameController.EntityHolder);

        return clone;
    }

    public override void CollectData(DataContainer container)
    {
        container.m_spawnable = m_Active;
    }

    public override void LoadData(DataContainer container)
    {

    }
}
