using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class FlashShootChokes : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, 0.1f);
        }

    }

}
