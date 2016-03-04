using UnityEngine;

namespace GalacticJanitor.Game
{
    public class AlienSpit : Projectile
    {
        public bool isEntraving;
        public int secondsToEntrave;

        protected override void OnHit(Collider hit)
        {
            
            if (hit.CompareTag("Player"))
            {

                PlayerController player = hit.GetComponent<PlayerController>();
                player.TakeDirectDamage(baseDamage);

                if (isEntraving)
                {
                    player.Entrave(secondsToEntrave);
                }

            }

            Destroy(gameObject);
        }
    } 
}
