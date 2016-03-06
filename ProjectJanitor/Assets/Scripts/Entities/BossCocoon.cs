using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;
using System;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(DeathLoot))]
    public class BossCocoon : MonoBehaviour
    {
        public bool combat;
        public int m_health;
        public AlienBoss m_boss;
        public Transform m_target;
        public Transform m_sensor;
        public Projectile m_projectile;

        public float m_projectileSpeed;
        public int m_projectileDamage;
        bool m_cooldownAttack;

        public AlienBase m_babies;
        public int m_babiesAmount;

        public AlienBase m_alienOnDeath;
        public AmmoBox m_dropAmmo;

        public int m_minimumDropAmmo;
        public int m_maximumDropAmmo;

        bool alive = true;

        public void Start()
        {
            m_target = GameController.Player.transform;
        }

        public int LifeLeach(int amount)
        {
            
            if (m_babiesAmount > 0)
            {
                SpawnBaby();
                m_babiesAmount--;
            }

            if (m_health < amount)
            {
                amount %= amount - m_health;
                m_health = 0;
            }
            else
            {
                m_health -= amount;
            }

            if (m_health <= 0 && alive)
            {
                alive = false;
                m_boss.StopInterphase();
                SpawnOnDeath();
                Destroy(gameObject);
            }
            
            return amount;
        }

        void Update()
        {
            if (m_sensor && m_target)
            {
                m_sensor.LookAt(m_target);
            }
        }

        IEnumerator Attack()
        {
            while(combat)
            {
                if (m_target)
                {
                    Projectile clone = Instantiate(m_projectile, m_sensor.position, m_sensor.rotation) as Projectile;
                    clone.AddSpeed(m_projectileSpeed);
                    clone.AddDamage(m_projectileDamage);
                }
                yield return new WaitForSeconds(2f);
            }
        }

        internal void StartAttack()
        {
            combat = true;
            StartCoroutine(Attack());
        }

        internal void Boost(int damage, float speed)
        {
            m_projectileDamage += damage;
            m_projectileSpeed += speed;
        }

        internal void SpawnOnDeath()
        {
            AlienBase alien = Instantiate(m_alienOnDeath, transform.position, transform.rotation) as AlienBase;
            alien.DisablePersistency();
            alien.m_entity.maxHealth *= 2;
            alien.m_entity.health *= 2;
            alien.LockTarget(m_target);
            m_boss.m_minions.Add(alien);

            AmmoBox drop = Instantiate(m_dropAmmo, transform.position, transform.rotation) as AmmoBox;
            drop.DisablePersistency();
            drop.useRandomAmount = true;
            drop.minRangeToRandom = m_minimumDropAmmo;
            drop.maxRangeToRandom = m_maximumDropAmmo;
        }

        void SpawnBaby()
        {
            AlienBase alien = Instantiate(m_babies, transform.position, transform.rotation) as AlienBase;
            alien.m_entity.maxHealth *= 2;
            alien.m_entity.health *= 2;
            alien.LockTarget(m_target);
            alien.DisablePersistency();
        }
    } 
}
