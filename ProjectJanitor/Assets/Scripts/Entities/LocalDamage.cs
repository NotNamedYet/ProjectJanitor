using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;
using System;

[RequireComponent(typeof(Collider))]
public class LocalDamage : MonoBehaviour, IDamageable {

    public LivingEntity m_entity;
    public int m_damageIndex;
    public bool m_reduced;

    public void TakeDirectDamage(int damage)
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

            if (damage > 0)
                m_entity.TakeDirectDamage(damage);
        }
    }

    public void TakeDirectDamage(int damage, bool ignoreArmor)
    {
        TakeDirectDamage(damage, false);
    }

    public void Die(){}
}
