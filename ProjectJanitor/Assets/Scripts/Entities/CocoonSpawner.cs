using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;
using System.Collections.Generic;

public class CocoonSpawner : Spawner, IDamageable
{

    [SerializeField]
    public LivingEntity.EntityBook m_Entity;
    public int m_MaxPoolSize;
    public float m_TimeBetweenSpawn;
    public bool m_DestroyAfterLast;
    public Animator m_Rigging;

    List<AlienBase> m_Pool;
    Transform m_Target;

    void Start()
    {
        m_Pool = new List<AlienBase>(m_MaxPoolSize);
        PopulatePool(m_MaxPoolSize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Debug.Log((m_Pool != null) ? m_Pool.Count.ToString() : "Null");
    }

    #region INTERNAL

    IEnumerator SpawnRoutine()
    {

        while (m_Active)
        {
            if (m_Rigging)
                yield return new WaitForSeconds(.3f);

            UnleachAlien();
            yield return new WaitForSeconds(m_TimeBetweenSpawn);
        }

    }

    void PopulatePool(int size)
    {
        StopAllCoroutines();

        m_Pool.Clear();

        for (int i = 0; i < m_MaxPoolSize; i++)
        {
            m_Pool.Add(CreateAlien(false));
        }
    }

    void ClearPool()
    {
        if (m_Pool != null)
        {
            if (m_Pool.Count > 0)
            {
                int dbg = 0;
                for (int i = 0; i < m_Pool.Count; i++)
                {
                    AlienBase toDestroy = m_Pool[i];

                    if (!toDestroy.gameObject.activeSelf)
                        Destroy(toDestroy.gameObject);

                    dbg++;
                }
                m_Pool.Clear();
                Debug.Log("cleaned " + dbg);
            }
        }
    }

    void UnleachAlien()
    {
        if (!m_Active) return;

        AlienBase clone = null;

        if (m_Pool.Count != 0)
        {
            clone = m_Pool[0];
            m_Pool.RemoveAt(0);

            if (m_Rigging)
                m_Rigging.SetTrigger("open");

            clone.gameObject.SetActive(true);

            if(m_Target)
                clone.LockTarget(m_Target);
        }
        else
        {
            if (m_DestroyAfterLast)
                Die();
        }
    }
    #endregion

    #region PUBLICS

    public void TriggerSpawning(Transform target)
    {
        if (target)
        {
            m_Target = target; 
        }
        StopAllCoroutines();
        StartCoroutine(SpawnRoutine());
    }

    public void TakeDirectDamage(int damage, bool ignoreArmor)
    {
        TakeDirectDamage(damage, false);
    }

    public void TakeDirectDamage(int damage)
    {
        if (m_Active)
        {
            m_Entity.health -= damage;
            if (m_Entity.health <= 0)
            {
                m_Entity.health = 0;
                Die();
            }
        }
    }

    public void Die()
    {
        m_Active = false;
        StopAllCoroutines();
        ClearPool();
        Save();

        Destroy(gameObject);
    }
    #endregion
}
