using UnityEngine;
using System.Collections;
using GalacticJanitor.Persistency;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(SavableAmmoBox))]
    public class AmmoBox : MonoBehaviour
    {

        [Range(0, 10)]
        public int amount;

        private string uniqueID;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (amount <= 0)
            {
                Destroy(gameObject);
            }
        }


    } 
}
