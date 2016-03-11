/*
    Optional DIRECTIVES : (not required to work well)  
       - Commented as SOUND for every line calling an optional audiosource.
       - Commented as UI or GUI for every line about UI/GUI
       - Commented as ANIM for animator variable trigger/param update.
    No Required Directive.
*/
using UnityEngine;
using System.Collections;
using GalacticJanitor.UI;
using System;

namespace GalacticJanitor.Game
{
    public abstract class LivingEntity : Entity, IDamageable
    {

        [System.Serializable]
        public class EntityBook
        {
            public bool alive = true;

            [Tooltip("Current Health point")]
            public int health = 20;

            [Tooltip("The maximum healh point")]
            public int maxHealth = 20;

            [Tooltip("Current armor point")]
            public int armor = 20;

            [Tooltip("The maximum armor Points")]
            public int maxArmor = 20;

            [Tooltip("Damages are divided by this number if the entity has at least one armor point")]
            public int armorDamageReduction = 2;
        }

        public int LastDamage { private set; get; }

        [Header("Living Behavior")]
        [SerializeField]
        public EntityBook m_entity;

        public bool destroyOnDeath = false;
        public bool saveOnDeath = true;
        public bool invincible = false;

        [Header("Animation")]
        [Tooltip("Animator passed as reference should have a trigger called 'hitted' to fire damage animation")]
        public Animator optionalAnimator;

        [Header("GUI")]
        public EntityResourceDisplay entityDisplay;

        [HideInInspector]
        public DeathLoot deathloot;

        [Header("Entity Sounds")]
        public AudioClip sndOnHit;
        public AudioClip sndOnDie;
        public AudioClip sndOnBreakingArmor;
        public AudioClip sndOnHeal;
        public AudioClip sndOnRepair;
        public AudioSource listener;
        public bool isPlayingOnHitSnd;

        protected virtual void Start()
        {
            UpdateDisplay();

            /*SOUND*/
            isPlayingOnHitSnd = false;
            listener = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Inflict the specified amount of damage to this entity. If the entity health falls to 0, the entity die.
        /// <br>
        /// Damage are divided by armorDamageReduction
        /// </br>
        /// </summary>
        /// <param name="damage">health point to loose</param>
        /// <param name="ignoreArmor">true to ignore armor damage reduction</param>
        public void TakeDirectDamage(int damage, bool ignoreArmor)
        {
            if (invincible || damage <= 0) return;

            if (m_entity.alive)
            {
                bool snd_armorIsBreakThisTime = false; // Use as flag to avoid double sounds
                if (m_entity.armor > 0 && !ignoreArmor)
                {
                    if (damage <= m_entity.armor)
                    {
                        m_entity.armor -= damage;
                        damage /= m_entity.armorDamageReduction;
                    }
                    else
                    {
                        //the damage is greater than armor points so we can take this value to determine how much damage need to be reduced 
                        int reduced = m_entity.armor / m_entity.armorDamageReduction;

                        //The rest represent the full damage to take...
                        int fullDamage = damage - m_entity.armor;

                        m_entity.armor = 0;

                        //Now we can calculate the damage.
                        damage = reduced + fullDamage;

                        /*SOUND*/
                        if (sndOnBreakingArmor)
                        {
                            listener.PlayOneShot(sndOnBreakingArmor);
                            snd_armorIsBreakThisTime = true;
                        }
                    }
                }

                LastDamage = damage;
                m_entity.health -= damage;

                OnDamaged();

                /*SOUND*/
                if (sndOnHit && !snd_armorIsBreakThisTime && !isPlayingOnHitSnd) StartCoroutine(CoroutPlaySndOnHit()); // If armor is breaking this frame, no hit sound

                /*ANIM*/
                if (optionalAnimator)
                    optionalAnimator.SetTrigger("hitted");

                if (m_entity.health <= 0)
                {
                    m_entity.health = 0;
                    Die();
                }

                /*GUI*/
                UpdateDisplay();
            }
        }

        #region Sounds

        IEnumerator CoroutPlaySndOnHit()
        {
            isPlayingOnHitSnd = true;
            listener.PlayOneShot(sndOnHit);
            yield return new WaitForSeconds(sndOnHit.length);
            isPlayingOnHitSnd = false;
        }

        #endregion

        /// <summary>
        /// Inflict the specified amount of damage to this entity. If the entity health falls to 0, the entity die.
        /// <br>
        /// Damage are divided by armorDamageReduction
        /// </br>
        /// </summary>
        /// <param name="damage">health point to loose</param>
        public void TakeDirectDamage(int damage)
        {
            TakeDirectDamage(damage, false);
        }

        protected virtual void OnDamaged() { }

        /// <summary>
        /// Heal the entity with the given amount. (result cannot exceed max Health value)
        /// </summary>
        /// <param name="amount">Health point to gain</param>
        /// <returns>false if the current health value is already at it's maximum</returns>
        public bool Heal(int amount)
        {
            if (m_entity.health >= m_entity.maxHealth)
                return false;

            m_entity.health += amount;
            if (m_entity.health > m_entity.maxHealth)
                m_entity.health = m_entity.maxHealth;

            /*SOUND*/
            if (sndOnHeal) listener.PlayOneShot(sndOnHeal);

            /*GUI*/
            UpdateDisplay();

            return true;
        }

        /// <summary>
        /// Heal this entity completely.
        /// </summary>
        /// <returns>false if the current health value is already at it's maximum</returns>
        public bool Heal()
        {
            return Heal(m_entity.maxHealth);
        }

        /// <summary>
        /// Repair the armor points of this entity by the given amount. (result cannot exceed max ArmorPoint value)
        /// </summary>
        /// <returns>false if the current AP value is already at it's maximum</returns>
        public bool RepairArmor(int amount)
        {
            if (m_entity.armor >= m_entity.maxArmor)
                return false;

            m_entity.armor += amount;

            if (m_entity.armor > m_entity.maxArmor)
                m_entity.armor = m_entity.maxArmor;

            /*SOUND*/
            if (sndOnRepair) listener.PlayOneShot(sndOnRepair);

            /*GUI*/
            UpdateDisplay();
            return true;
        }

        /// <summary>
        /// Repair the armor points to maximum.
        /// </summary>
        /// <returns>false if the current AP value is already at it's maximum</returns>
        public bool RepairArmor()
        {
            return RepairArmor(m_entity.maxArmor);
        }

        /// <summary>
        /// Kill the entity. Destroy the gameoject if destroyOnDeath is set to true
        /// </summary>
        public void Die()
        {

            m_entity.alive = false;

            if (saveOnDeath) Save();

            /*SOUND*/
            if (sndOnDie)
            {
                listener.Stop();
                listener.PlayOneShot(sndOnDie);
            }

            //Child ... 
            OnDeath();

            /*GUI*/
            UpdateDisplay();

            if (destroyOnDeath)
            {
                if (deathloot)
                    deathloot.SpawnLoot();

                Destroy(gameObject);
            }
        }

        protected virtual void OnDeath() { }

        /// <summary>
        /// Instant kill this entity. Don't overpass destroyOnDeath behavior
        /// </summary>
        public void Kill()
        {
            Kill(false);
        }

        /// <summary>
        /// /// Instant kill this entity. 
        /// </summary>
        /// <param name="forceDestroyOnDeath">force to destroy on death if true</param>
        public void Kill(bool forceDestroyOnDeath)
        {
            if (forceDestroyOnDeath) destroyOnDeath = true;
            Die();
        }

        /// <summary>
        /// Kill this entity over the given time in seconds. Don't overpass destroyOnDeath behavior
        /// </summary>
        /// <param name="delay"></param>
        public void Kill(float delay)
        {
            Invoke("Kill", delay);
        }

        /// <summary>
        /// Kill this entity over the given time in seconds.
        /// </summary>
        /// <param name="forceDestroyOnDeath">force to destroy on death if true</param>
        /// <param name="delay">Delay in seconds</param>
        public void Kill(bool forceDestroyOnDeath, float delay)
        {
            if (forceDestroyOnDeath)
                StartCoroutine(KillAfterDelay(delay));
            else Kill(delay);
        }

        /// <summary>
        /// Kill couroutine for force destroy + delay...
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        IEnumerator KillAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            Kill(true);
        }

        /*GUI*/
        /// <summary>
        /// To call when a GUI update is needed...
        /// </summary>
        public virtual void UpdateDisplay()
        {
            if (entityDisplay) entityDisplay.UpdateState(this);
        }
    } 
}
