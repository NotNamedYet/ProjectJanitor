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

        [Header("Phase management")]
        public float m_speedAddition = 2;
        [HideInInspector] public bool m_interphase;

        [Header("Minions")]
        [HideInInspector]
        public List<AlienBase> m_minions;
        public List<BossCocoon> m_cocoons;
        public int m_cocoonBoostValue = 10;
        bool m_targetSpreadedToCocoons;

        [Header("End Game")]
        public List<Interactable> m_onDeathInteractables;
        public GameObject m_endGameActor;

        [Header("Attack Behavior")]
        public float m_TimeBetweenSpin;
        bool m_attackCooldown;
        bool m_SpinActive;

        #region MONOBVR

        protected override void Start()
        {
            base.Start();
            player = GameController.Player.transform;
            m_minions = new List<AlienBase>();
        }

        #endregion

        #region INTERPHASE_MGT

        /// <summary>
        /// Start boss interphase. The Mother becomes invincible and go regenrate by eating a cocoon.
        /// Apply some extra boost and effects.
        /// </summary>
        void StartInterphase()
        {
            if (!m_interphase)
            {
                m_interphase = true;

                EnrageMinions();

                GameController.NotifyPlayer("The Mother calls for retribution !", Color.red, 2);

                invincible = true;
                pathfinder.speed = pathfinder.speed + 1f;
                BoostCocoons();

                if (m_cocoons != null && m_cocoons.Count > 0)
                {
                    BossCocoon cocoon = m_cocoons[0];
                    LockTarget(cocoon.transform);
                    m_cocoons.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// The mother re-engage the player.
        /// </summary>
        internal void StopInterphase()
        {
            TranquilizeMinions();
            GameController.NotifyPlayer("The Mother comes for you...", Color.red, 2);
            m_interphase = false;
            invincible = false;
            LockTarget(player);
        }

        #endregion

        #region MINION_MGT

        internal void BoostCocoons()
        {
            foreach(BossCocoon b in m_cocoons)
            {
                b.Boost(m_cocoonBoostValue / m_cocoons.Count, .5f);
            }
        }

        /// <summary>
        /// Enrage them all, for interphase madness !! 
        /// </summary>
        public void EnrageMinions()
        {
            foreach(AlienBase minion in m_minions)
            {
                if (minion != null)
                {
                    minion.Enrage();
                }
            }
        }

        public void TranquilizeMinions()
        {
            foreach (AlienBase minion in m_minions)
            {
                if (minion != null)
                {
                    minion.Traquilize();
                }
            }
        }

        #endregion

        #region SPIN_ATTACK

        IEnumerator SpinAttackRoutine()
        {
            m_SpinActive = true;

            while(m_SpinActive)
            {
                yield return new WaitForSeconds(m_TimeBetweenSpin);

                if (target)
                    SpinAttack();
            }
        }

        void SpinAttack()
        {
            rigging.SetTrigger("spin");
        }

        #endregion

        #region OVERRIDES

        /// <summary>
        /// Start interphase at 50% of HP
        /// </summary>
        protected override void OnDamaged()
        {
            base.OnDamaged();

            int percent = (m_entity.health * 100 / m_entity.maxHealth);

            if (percent < 50 && m_cocoons.Count > 0)
            {
                StartInterphase();
            }
        }

        /// <summary>
        /// Normal attack is handle here
        /// </summary>
        protected override void Attack()
        {
            if (rigging && !m_attackCooldown)
            {
                m_attackCooldown = true;
                rigging.SetTrigger("attack");
                Invoke("ReleaseCooldown", attackPerSecond);
            }
        }

        void ReleaseCooldown()
        {
            m_attackCooldown = false;
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            destroyOnDeath = false;

            if (rigging)
            {
                //The destroy is controlled by the animation.
                rigging.SetTrigger("onDeath");
            }
            else
            {
                Destroy(gameObject);
            }

            //Sesame open you... (or not...)
            if (m_onDeathInteractables != null)
                foreach (Interactable inter in m_onDeathInteractables)
                    inter.OnInteract();

            if (m_endGameActor)
                m_endGameActor.SetActive(true);

        }

        /// <summary>
        /// Regular targetting + give the order of attack to all cocoons
        /// </summary>
        /// <param name="target"></param>
        public override void SetTarget(Transform target)
        {
            base.SetTarget(target);

            if (!m_targetSpreadedToCocoons && m_cocoons != null)
            {
                StartCoroutine(SpinAttackRoutine());
                foreach (BossCocoon b in m_cocoons)
                    b.StartAttack();

                m_targetSpreadedToCocoons = true;
            }
        }

        #endregion

    }
}
