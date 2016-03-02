using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{
    public class AlienBoss : AlienBase
    {
        public Transform player;
        public bool m_interphase;
        public float m_speedAddition = 2;

        public List<BossCocoon> m_cocoons;
        public int m_cocoonBoostValue = 10;

        [SerializeField]
        public List<Interactable> m_onDeathInteractables;

        bool attackCooldown;

        protected override void Start()
        {
            base.Start();
            player = GameController.Player.transform;
        }

        void StartInterphase()
        {
            if (!m_interphase)
            {
                m_interphase = true;

                GameController.NotifyPlayer("The Mother calls for retribution !", Color.red, 2);

                invincible = true;
                pathfinder.speed = pathfinder.speed + 2f;
                BoostCocoons();

                if (m_cocoons != null && m_cocoons.Count > 0)
                {
                    BossCocoon cocoon = m_cocoons[0];
                    LockTarget(cocoon.transform);
                    m_cocoons.RemoveAt(0);
                }
            }
        }

        internal void StopInterphase()
        {
            GameController.NotifyPlayer("The Mother comes for you...", Color.red, 2);
            m_interphase = false;
            invincible = false;
            LockTarget(player);
        }

        public override void TakeDirectDamage(int damage, bool ignoreArmor)
        {
            base.TakeDirectDamage(damage, ignoreArmor);

            int percent = (m_entity.health * 100 / m_entity.maxHealth);

            if (percent < 50 && m_cocoons.Count > 0)
            {
                StartInterphase();
            }
        }

        protected override void Attack()
        {
            if (rigging && !attackCooldown)
            {
                attackCooldown = true;
                rigging.SetTrigger("attack");
                Invoke("ReleaseCooldown", attackPerSecond);
            }    
        }

        void ReleaseCooldown()
        {
            attackCooldown = false;
        }

        bool spreaded;
        public override void SetTarget(Transform target)
        {
            base.SetTarget(target);
            if (!spreaded && m_cocoons != null)
            {
                foreach (BossCocoon b in m_cocoons)
                    b.StartAttack();

                spreaded = true;
            }
        }

        internal void BoostCocoons()
        {
            foreach(BossCocoon b in m_cocoons)
            {
                b.Boost(m_cocoonBoostValue / m_cocoons.Count, m_cocoonBoostValue / m_cocoons.Count);
            }
        }

        public GameObject endGameActor;

        protected override void Die()
        {
            base.Die();

            if (m_onDeathInteractables != null)
                foreach (Interactable inter in m_onDeathInteractables)
                    inter.OnInteract();

            if (endGameActor)
                endGameActor.SetActive(true);
        }

    } 
}
