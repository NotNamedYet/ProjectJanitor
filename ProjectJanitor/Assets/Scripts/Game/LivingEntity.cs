using UnityEngine;
using System.Collections;


namespace GalacticJanitor.Game
{
    public class LivingEntity : MonoBehaviour
    {
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

        /// <summary>
        /// Inflict the specified amount of damage to this entity. If the entity health falls to 0, the entity die.
        /// <br>
        /// Damage are divided by armorDamageReduction
        /// </br>
        /// </summary>
        /// <param name="damage">health point to loose</param>
        public void TakeDirectDamage(int damage)
        {
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
                    }
                }

                health -= damage;
                if (health <= 0)
                {
                    health = 0;
                    Die();
                }
            }
        }

        /// <summary>
        /// Heal the entity with the givent amount of life.
        /// </summary>
        /// <param name="amount">Health point to gain</param>
        public void Heal(int amount)
        {
            health += amount;
            if (health >= maxHealth)
                ResetHealth();
        }

        /// <summary>
        /// Reset the armor point to it's maximum value.
        /// </summary>
        /// <returns>false if the current AP value is already at it's maximum</returns>
        public bool RepairArmor()
        {
            if (armorPoint == maxArmorPoint)
                return false;

            armorPoint = maxArmorPoint;
            return true;
        }

        /// <summary>
        /// Reset the current health to it's maximum value
        /// </summary>
        public void ResetHealth()
        {
            health = maxHealth;
        }

        /// <summary>
        /// Kill the entity. Destroy the gameoject if destroyOnDeath is set to true
        /// </summary>
        public void Die()
        {
            alive = false;
            if (destroyOnDeath)
                Destroy(gameObject);
        }
    } 
}
