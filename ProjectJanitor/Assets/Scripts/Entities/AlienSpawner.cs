using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;
using System.Collections.Generic;
using MonoPersistency;
using System;

[RequireComponent(typeof(SphereCollider))]
public class AlienSpawner : MonoPersistent {

    public AlienBase m_alien;
    public int m_AmountToSpawn;
    public Transform m_spawnLocation;

    List<AlienBase> m_pool;
    Transform m_target;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Collider>().isTrigger = true;

        if (!m_spawnLocation) m_spawnLocation = transform;

        m_pool = new List<AlienBase>(m_AmountToSpawn);

        FillPool();
	}

    void FillPool()
    {
        for (int i = 0; i < m_AmountToSpawn; i++)
        {

            AlienBase clone = Instantiate(m_alien, m_spawnLocation.position, m_spawnLocation.rotation) as AlienBase;
            clone.DisablePersistency();
            clone.gameObject.SetActive(false);

            m_pool.Add(clone);
        }
    } 

    void UnleachAliens()
    {
        if (m_pool != null)
        {
            for(int i = 0; i < m_pool.Count; i++)
            {
                AlienBase toUnleach = m_pool[i];
                m_pool.RemoveAt(i);
                toUnleach.gameObject.SetActive(true);
                toUnleach.LockTarget(m_target);
            }
        }
        Save();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_target = other.transform;
            UnleachAliens();
        }
    }

    void OnDestroy()
    {
        if (m_pool != null)
            foreach (AlienBase alien in m_pool)
                if (alien) Destroy(alien.gameObject);
    }

    public override void CollectData(DataContainer container)
    {
        container.m_spawnable = false;
    }

    public override void LoadData(DataContainer container) { }
}
