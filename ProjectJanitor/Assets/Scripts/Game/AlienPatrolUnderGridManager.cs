using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class AlienPatrolUnderGridManager : MonoBehaviour
    {

        [Tooltip("Manage there the ref to aliens you want in the patrol")]
        public AlienMoveUnderGrid[] patrol;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") RunBabiesRun();
        }

        void RunBabiesRun()
        {
            foreach (AlienMoveUnderGrid babie in patrol){ babie.gameObject.SetActive(true); }
        }
    } 
}
