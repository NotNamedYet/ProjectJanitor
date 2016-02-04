using UnityEngine;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{

    public class AlienPatrolUnderGridManager : MonoBehaviour
    {

        [Tooltip("Manage there the ref to aliens you want in the patrol")]
        public List<AlienMoveUnderGrid> patrol;

        void Update()
        {
            if (patrol.Count == 0) Destroy(gameObject); // Each alien have a function to remove itself from patrol List, when it reaches its goal
        }

        public void RunBabiesRun()
        {
            foreach (AlienMoveUnderGrid babie in patrol){ babie.gameObject.SetActive(true); }
        }

    } 

}
