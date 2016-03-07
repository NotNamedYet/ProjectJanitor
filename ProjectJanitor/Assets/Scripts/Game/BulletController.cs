using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;
using System;

namespace GalacticJanitor.Game
{
    public class BulletController : Projectile
    {

        public GameObject damageSource;

        public void SetSource(GameObject source)
        {
            damageSource = source;
        }

        protected override void OnHit(RaycastHit hit)
        {

            IDamageable damageable = hit.collider.GetComponent(typeof(IDamageable)) as IDamageable;

            if (damageable != null)
            {
                damageable.TakeDirectDamage(baseDamage);

                if (damageable is LocalDamage)
                {
                    AlienBase alien = ((damageable as LocalDamage).m_entity) as AlienBase;

                    if (alien)
                        alien.SetTarget(damageSource.transform);
                }
                else if (damageable is CocoonSpawner)
                {
                    (damageable as CocoonSpawner).TriggerSpawning(damageSource.transform);
                }
                else if (damageable is AlienBase)
                {
                    (damageable as AlienBase).SetTarget(damageSource.transform);
                }
            } 

            Destroy(gameObject);
        }
    }
}