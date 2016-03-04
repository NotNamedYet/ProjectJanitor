using UnityEngine;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{

    public class AlienPatrolUnderGridManager : MonoBehaviour
    {

        [Tooltip("Manage there the ref to aliens you want in the patrol")]
        public List<AlienMoveUnderGrid> patrol;

        public void RunBabiesRun()
        {
            foreach (AlienMoveUnderGrid babie in patrol){ babie.gameObject.SetActive(true); }
        }

    } 

}
