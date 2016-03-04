using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class TriggerPatrolUnderGrid : MonoBehaviour
    {

        AudioSource listener;
        private bool flagAlreadyPlayedSnd = false;

        void Awake()
        {
            listener = GetComponent<AudioSource>();
        }

        [Tooltip("If no ref, do it with the pfb of the patrol")]
        public AlienPatrolUnderGridManager pat;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                pat.RunBabiesRun();
                if (!flagAlreadyPlayedSnd)
                {
                    listener.Play();
                    flagAlreadyPlayedSnd = true;
                }
            }
        }
    }

}