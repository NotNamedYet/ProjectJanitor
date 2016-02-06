using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    /// <summary>
    /// // WARNING : Put this script in the gameobject called : BaseRotation
    /// </summary>
    public class PlayerRotation : MonoBehaviour
    {
        bool pause;

        /// <summary>
        /// Used by PlayerController to avoid the alzheimer's bug
        /// </summary>
        public void ForceLookAt()
        {
            //if (!GalacticJanitor.Engine.GameController.Controller.isInPause) transform.LookAt(PointerTracker.MousePosition);
            if (!pause) transform.LookAt(PointerTracker.MousePosition);
        }

        void OnEnable()
        {
            PauseManager.OnPause += ToggleRotation;
        }

        void OnDisable()
        {
            PauseManager.OnPause -= ToggleRotation;
        }

        void ToggleRotation()
        {
            pause ^= true;
        }

    }

}