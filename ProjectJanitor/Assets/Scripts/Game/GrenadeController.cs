using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class GrenadeController : Projectile
    {

        public GrenadeExplosion explosion;
        public GameObject damageSource;

        public void SetSource(GameObject source)
        {
            damageSource = source;
        }

        protected override void OnHit(RaycastHit hit)
        {
            GrenadeExplosion explo = Instantiate(explosion, transform.position, transform.rotation) as GrenadeExplosion;

            explo.explosionDmg = baseDamage;
            explo.damageSource = damageSource;

            Destroy(gameObject);
        }
    } 
}
