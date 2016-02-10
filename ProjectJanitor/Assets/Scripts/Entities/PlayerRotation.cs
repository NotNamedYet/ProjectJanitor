using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

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
            if (!pause) transform.LookAt(TopDownCamera.MousePosition);
        }

        void OnEnable()
        {
            GameController.IsPause();
        }

        void OnDisable()
        {
            GameController.IsPause();
        }

        void ToggleRotation()
        {
            pause ^= true;
        }

    }

}