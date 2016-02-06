using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    /// <summary>
    /// // WARNING : Put this script in the gameobject called : BaseRotation
    /// </summary>
    public class PlayerRotation : MonoBehaviour
    {
        /*void Update()
        {
            transform.LookAt(PointerTracker.MousePosition);
        }*/

        /// <summary>
        /// Used by PlayerController to avoid the alzheimer's bug
        /// </summary>
        public void ForceLookAt()
        {
            if(!GalacticJanitor.Engine.GameController.Controller.isInPause) transform.LookAt(PointerTracker.MousePosition);
        }
    }

}