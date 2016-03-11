using UnityEngine;
using System.Collections;
using MonoPersistency;
using System;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MonoPersistentInitializer))]
public class ObjectActivationTrigger : MonoPersistent {

    public GameObject m_ToActivate;
    bool waiting = true;

    void Start()
    {
        LoadData();

        if (!waiting)
        {
            if (m_ToActivate)
                Destroy(m_ToActivate);

            Destroy(gameObject);
        }
    }

    public override void CollectData(DataContainer container)
    {
        container.m_spawnable = true;
        container.Addvalue("waiting", waiting);
    }

    public override void LoadData(DataContainer container)
    {
        try
        {
            waiting = container.GetValue<bool>("waiting");
        }
        catch(Exception)
        {
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !m_ToActivate)
            return;

        waiting = false;
        m_ToActivate.SetActive(true);
        Save();
        Destroy(gameObject);
    }

}
