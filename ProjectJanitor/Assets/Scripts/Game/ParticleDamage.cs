using UnityEngine;
using System.Collections.Generic;
using GalacticJanitor.Game;

public class ParticleDamage : MonoBehaviour {

    public int m_DamageAmount;
    public bool m_EntravePlayer;
    public int m_EntraveDuration = 2;

    //Dictionary<int, IDamageable> cache;

    void Start()
    {
        //cache = new Dictionary<int, IDamageable>();
    }

    void OnParticleCollision(GameObject other)
    {
        IDamageable damageable = other.GetComponent(typeof(IDamageable)) as IDamageable;

        if (damageable != null)
        {
            damageable.TakeDirectDamage(m_DamageAmount, true);

            if (m_EntravePlayer && damageable is PlayerController)
                other.GetComponent<PlayerController>().Entrave(m_EntraveDuration);
        }
    }

    /*void OnParticleCollision(GameObject other)
    {
        IDamageable damageable;

        if (cache.ContainsKey(other.GetInstanceID()))
        {
            damageable = cache[other.GetInstanceID()];
        }
        else
        {
            damageable = other.GetComponent(typeof(IDamageable)) as IDamageable;
            cache.Add(other.GetInstanceID(), damageable);
        }

        if (damageable != null)
        {
            damageable.TakeDirectDamage(m_DamageAmount, true);
        } 

    }*/

    void OnDestroy()
    {
        //cache.Clear();
    }
}
