using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    // WARNING : Put this script in the gameobject called : BaseRotation
    public class PlayerRotation : MonoBehaviour
    {
        void Update()
        {
            transform.LookAt(PointerTracker.MousePosition);
        }
    }

}