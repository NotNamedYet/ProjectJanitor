using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class FlashChokesController : MonoBehaviour
    {

        [Tooltip("Time before desactivation")]
        public float desactivateTimer;

        private ParticleSystem part;

        void Awake()
        {
            part = GetComponent<ParticleSystem>();
            gameObject.SetActive(false);
        }

        void OnEnable()
        {
            Invoke("Desactive", desactivateTimer);
            part.Play();
        }

        void Desactive()
        {
            gameObject.SetActive(false);
        }
    }

}