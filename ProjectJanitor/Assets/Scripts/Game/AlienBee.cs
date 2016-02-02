using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class AlienBee : AlienBase
    {

        public int damagePerHit;
        public float dammageDistanceOffset;
        float nextAttack;
        bool firstAttack;

        protected override void Attack()
        {
            if (Time.time > nextAttack)
            {
                if (rigging)
                {
                    rigging.SetTrigger((firstAttack) ? "attack1" : "attack2");
                    firstAttack ^= true;

                    /*SOUND*/
                    if (onAttackSound) onAttackSound.Play();
                }
                if (target.gameObject.tag == "Player")
                {
                    LivingEntity entity = target.GetComponent<LivingEntity>();

                    if (entity != null && GetDistanceFromTarget() < maxAttackRange + dammageDistanceOffset)
                    {
                        entity.TakeDirectDamage(damagePerHit);
                    }
                }

                nextAttack = Time.time + (enraged ? attackPerSecond / enrageModifier : attackPerSecond);
            }
        }
    } 
}
