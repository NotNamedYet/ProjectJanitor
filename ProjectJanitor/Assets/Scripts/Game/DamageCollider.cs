using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;

[RequireComponent(typeof(Collider))]
public class DamageCollider : MonoBehaviour {

    public LayerMask m_colliderMask;
    public int m_damage;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	void OnTriggerEnter(Collider other)
    {
        if ((m_colliderMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            LivingEntity entity = other.GetComponent<LivingEntity>();

            if (entity)
            {
                entity.TakeDirectDamage(m_damage);
            }
        }
    }
}
