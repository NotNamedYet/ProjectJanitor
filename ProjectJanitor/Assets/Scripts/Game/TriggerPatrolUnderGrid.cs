using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class TriggerPatrolUnderGrid : MonoBehaviour
    {

        [Tooltip("If no ref, do it with the pfb of the patrol")]
        public AlienPatrolUnderGridManager pat;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") pat.RunBabiesRun();
        }
    }

}