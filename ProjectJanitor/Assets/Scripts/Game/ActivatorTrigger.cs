using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MonoPersistency;
using System;

[RequireComponent(typeof(Collider))]
public class ActivatorTrigger : MonoPersistent {

    public LayerMask m_colliderMask;
    
    public Interactable m_actor;
    [SerializeField]
    public List<Interactable> m_multiActors;

    public bool oneUseOnly;
    public bool persistent;
    bool m_unused = true;

	// Use this for initialization
	void Start ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if ((m_colliderMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            if (m_actor)
            {
                m_actor.OnInteract();
            }
            if (m_multiActors != null)
            {
                foreach (Interactable inter in m_multiActors)
                {
                    inter.OnInteract();
                }
            }
            if (oneUseOnly)
            {
                if (persistent)
                {
                    m_unused = false;
                    Save();
                }
                Destroy(gameObject);
            }
        }
    }

    public override void CollectData(DataContainer container)
    {
        container.m_spawnable = m_unused;
    }

    public override void LoadData(DataContainer container){}
}
