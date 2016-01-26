using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class AlienBeta : AlienBase
    {

        public int damagePerHit;
        public float dammageDistanceOffset;
        float nextAttack;

        protected override void Attack()
        {
            if (Time.time > nextAttack)
            {
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
