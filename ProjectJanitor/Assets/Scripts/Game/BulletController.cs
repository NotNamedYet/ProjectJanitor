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
            LocalDamage localDamaged = hit.collider.GetComponent<LocalDamage>();

            if (localDamaged != null)
            {
                localDamaged.TakeDirectDamage(baseDamage);
                Debug.Log("locally damaged");
                if (localDamaged.m_entity is AlienBase)
                {
                    AlienBase alien = localDamaged.m_entity as AlienBase;

                    if (alien.target == null)
                    {
                        alien.SetTarget(damageSource.transform);
                    }

                }
            }
            else if (hit.collider.CompareTag("Alien"))
            {
                AlienBase entity = hit.collider.GetComponent<AlienBase>(); // Ref to AlienBase to access target field
                entity.TakeDirectDamage(baseDamage);

                if (entity.target == null)
                {
                    entity.SetTarget(damageSource.transform);
                }
            }
            Destroy(gameObject);
        }
    }
}