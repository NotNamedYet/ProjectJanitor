using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(Collider))]
    public class TeleporterTrigger : MonoBehaviour
    {

        public Animator animator;
        
        void OnTriggerEnter(Collider other)
        {
            if (animator != null)
            {
                if (other.gameObject.tag == "Player")
                {
                    animator.SetBool("isOn", true);
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (animator != null)
            {
                if (other.gameObject.tag == "Player")
                {
                    animator.SetBool("isOn", false);
                }
            }
        }
    } 
}
