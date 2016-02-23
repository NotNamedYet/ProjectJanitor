using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;

public class DeathLoot : MonoBehaviour {

    [Range(0f, 1f)]
    public float m_lootChances;

    public int m_minRandomAmount;
    public int m_maxRandomAmount;
    public ResourceLoot m_armor;
    public ResourceLoot m_health;

	// Use this for initialization
	void Start ()
    {
        if (GetComponent<LivingEntity>() != null)
            GetComponent<LivingEntity>().deathloot = this;
	}

    public void SpawnLoot()
    {
        if (Random.Range(0f, 1f) < m_lootChances)
        {
            int amount = Random.Range(m_minRandomAmount, m_maxRandomAmount);

            float dice = Random.Range(0f, 1f);

            ResourceLoot clone = Instantiate((dice >= .5f) ? m_armor : m_health, transform.position, transform.rotation) as ResourceLoot;
            clone.useRandomAmount = false;
            clone.amount = amount;

        }
        
    }
}
