using UnityEngine;
using System.Collections;
using MonoPersistency;
using System;
using GalacticJanitor.Engine;

[RequireComponent(typeof(SphereCollider))]
public class CocoonTrigger : MonoPersistent {

    [Header("Cocoon Settings.")]
    public bool m_DirectFocus = true;
    public bool playSoundOnActivation;
    [Tooltip("Check it if you want play a sound randomly from the scene ambiance manager")]
    public bool playSoundRandomlyWithSceneSounds;
    public AudioClip snd;

    public CocoonSpawner[] m_LinkedCocoons;
    bool m_Active = true;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Collider>().isTrigger = true;
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_LinkedCocoons.Length > 0)
                for (int i = 0; i < m_LinkedCocoons.Length; i++)
                    if (m_LinkedCocoons[i]) m_LinkedCocoons[i].TriggerSpawning((m_DirectFocus)?other.transform : null);

            /*SOUND*/
            if (playSoundOnActivation && snd) PlaySound();

            m_Active = false;
            Save();
            Destroy(gameObject);
        }
    }

    public override void CollectData(DataContainer container)
    {
        container.m_spawnable = m_Active;
    }

    public override void LoadData(DataContainer container){}

    #region SOUND
    private void PlaySound()
    {
        if (playSoundRandomlyWithSceneSounds) GameController.SceneSounds.PlayAmbianceSoundRandomlyOneShot();
        else GameController.SceneSounds.listenerAmbiance.PlayOneShot(snd);
    }
    #endregion
}
