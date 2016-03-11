using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleDamage))]
public class GasLeakToggler : MonoBehaviour {

    public float m_activationDelay;
    public float m_cooldown;
    ParticleSystem m_particleSystem;
    ParticleDamage m_particleDamage;


    void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
       
        if (!m_particleSystem)
        {
            Debug.Log(GetType().Name + " : No particle system attached. Disabling");
            enabled = false;
        }

        m_particleDamage = GetComponent<ParticleDamage>();
        m_particleDamage.enabled = false;

        Invoke("StartEffect", m_activationDelay);
    }

    void StartEffect()
    {
        if (m_particleSystem)
            StartCoroutine(GazLeakRoutine());
    }

    IEnumerator GazLeakRoutine()
    {
        OnEffectPlay();
        yield return new WaitForSeconds(m_particleSystem.duration);
        OnEffectStop();

        yield return new WaitForSeconds(m_cooldown);
        StartEffect();
    }

    void OnEffectPlay()
    {
        m_particleSystem.Play();
        m_particleDamage.enabled = true;

        ParticleSystem.CollisionModule module = m_particleSystem.collision;
        module.enabled = true;
    }

    void OnEffectStop()
    {
        m_particleDamage.enabled = false;

        ParticleSystem.CollisionModule module = m_particleSystem.collision;
        module.enabled = false;
    }


}
