using UnityEngine;

namespace GalacticJanitor.Game
{
    public class AlienSpit : Projectile
    {
        public bool isEntraving;
        public int secondsToEntrave;

        protected override void OnHit(RaycastHit hit)
        {
            
            if (hit.collider.CompareTag("Player"))
            {

                PlayerController player = hit.collider.GetComponent<PlayerController>();
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
