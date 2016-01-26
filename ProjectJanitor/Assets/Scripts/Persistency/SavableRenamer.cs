using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Persistency
{
    public class SavableRenamer : MonoBehaviour
    {
        Savable sav;

        void Update()
        {
            sav = GetComponent<Savable>();
            if (sav.savableObject != null)
                gameObject.name = "sv." + sav.savableObject.name;
        }
    } 
}
