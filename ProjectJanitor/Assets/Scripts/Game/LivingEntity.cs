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

namespace GalacticJanitor.Game
{
    public class LivingEntity : MonoBehaviour
    {
        [Header("Behavior")]
        [Tooltip("Current Health point")]
        public int health = 20;

        [Tooltip("The maximum heal point")]
        public int maxHealth = 20;

        [Tooltip("Current armor point")]
        public int armorPoint = 0;

        [Tooltip("The maximum armor Points")]
        public int maxArmorPoint = 0;

        [Tooltip("Damages are divided by this number if the entity has at least one armor point")]
        public int armorDamageReduction = 3;

        public bool alive = true;
        public bool destroyOnDeath = false;
        public bool invincible = false;

        [Header("Animation")]
        [Tooltip("Animator passed as reference should have a trigger called 'hitted' to fire damage animation")]
        public Animator optionalAnimator;

        [Header("GUI")]
        public EntityResourceDisplay optionalDisplay;

        [Header("Sounds")]
        public AudioSource onDamageSound;
        public AudioSource onDieSound;
        public AudioSource onArmorBreakSound;
        public AudioSource onHealSound;
        public AudioSource onRepairSound;

        void Start()
        {
            UpdateDisplay();
        }

        /// <summary>
        /// Inflict the specified amount of damage to this entity. If the entity health falls to 0, the entity die.
        /// <br>
        /// Damage are divided by armorDamageReduction
        /// </br>
        /// </summary>
        /// <param name="damage">health point to loose</param>
        public void TakeDirectDamage(int damage)
        {
            if (invincible) return;

            if (alive)
            {
                if (armorPoint > 0)
                {
                    if (damage <= armorPoint)
                    {
                        armorPoint -= damage;
                        damage /= armorDamageReduction;
                    }
                    else
                    {
                        //the damage is greater than armor points so we can take this value to determine how much damage need to be reduced 
                        int reduced = armorPoint / armorDamageReduction;

                        //The rest represent the full damage to take...
                        int fullDamage = damage - armorPoint;

                        armorPoint = 0;

                        //Now we can calculate the damage.
                        damage = reduced + fullDamage;

                        /*SOUND*/
                        if (onArmorBreakSound)
                            onArmorBreakSound.Play();
                    }
                }

                health -= damage;

                /*SOUND*/
                if (onDamageSound)
                    onDamageSound.Play();

                /*ANIM*/
                if (optionalAnimator)
                    optionalAnimator.SetTrigger("hitted");

                if (health <= 0)
                {
                    health = 0;
                    Die();
                }

                /*GUI*/
                UpdateDisplay();
            }
        }

        /// <summary>
        /// Heal the entity with the given amount. (result cannot exceed max Health value)
        /// </summary>
        /// <param name="amount">Health point to gain</param>
        /// <returns>false if the current health value is already at it's maximum</returns>
        public bool Heal(int amount)
        {
            if (health >= maxHealth)
                return false;

            health += amount;
            if (health > maxHealth)
                health = maxHealth;

            /*SOUND*/
            if (onHealSound)
                onHealSound.Play();

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
            return Heal(maxHealth);
        }

        /// <summary>
        /// Repair the armor points of this entity by the given amount. (result cannot exceed max ArmorPoint value)
        /// </summary>
        /// <returns>false if the current AP value is already at it's maximum</returns>
        public bool RepairArmor(int amount)
        {
            if (armorPoint >= maxArmorPoint)
                return false;

            armorPoint += amount;

            if (armorPoint > maxArmorPoint)
                armorPoint = maxArmorPoint;

            /*SOUND*/
            if (onRepairSound)
                onRepairSound.Play();

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
            return RepairArmor(maxArmorPoint);
        }

        /// <summary>
        /// Kill the entity. Destroy the gameoject if destroyOnDeath is set to true
        /// </summary>
        void Die()
        {
            alive = false;


            /*SOUND*/
            if (onDieSound)
                onDieSound.Play();

            /*GUI*/
            UpdateDisplay();

            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }

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
        void UpdateDisplay()
        {
            if (optionalDisplay) optionalDisplay.UpdateState(this);
        }
    } 
}
