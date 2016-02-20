using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;

[RequireComponent(typeof(Collider))]
public class LocalDamage : MonoBehaviour {

    public LivingEntity m_entity;
    public int m_damageIndex;
    public bool m_reduced;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void HitEntity(int damage)
    {
        if (m_entity)
        {
            if (m_reduced)
            {
                damage -= m_damageIndex;
            }
            else
            {
                damage += m_damageIndex;
            }

            Debug.Log(damage);

            if (damage > 0)
                m_entity.TakeDirectDamage(damage);
        }
    }
}
