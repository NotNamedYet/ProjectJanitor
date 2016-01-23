using UnityEngine;

namespace GalacticJanitor.Game
{
    public class AlienAggro : MonoBehaviour
    {

        public AlienBase alien;

        void OnTriggerEnter(Collider other)
        {
            if (alien == null) return;

            if (other.gameObject.tag == "Player")
                OnAggro(other.transform);

        }

        void OnAggro(Transform trackableTarget)
        {
            if (alien.target == null)
            {
                alien.target = trackableTarget;
            }
        }
    } 
}
