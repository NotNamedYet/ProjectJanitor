using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class TeleporterVisual : MonoBehaviour
    {

        public Animator animator;

        public bool asleep;
        public bool isOn;

        void Start()
        {
            if (isOn) Activation();
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
                Activation();
        }

        public void Activation()
        {
            if (!asleep)
            {
                ActivateAnimator(true);
            }
        }

        public void Desactivation()
        {
            ActivateAnimator(false);
        }

        public void SetAwake()
        {
            asleep = false;
        }

        public void SetAsleep(bool value)
        {
            asleep = value;
            if (value)
                Desactivation();
        }

        private void ActivateAnimator(bool value)
        {
            isOn = value;
            if (animator != null)
            {
                animator.SetBool("isOn", value);
            }
        }
    } 
}
