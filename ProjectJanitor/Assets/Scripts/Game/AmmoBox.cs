using UnityEngine;
using System.Collections;
using GalacticJanitor.Persistency;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(SavableAmmoBox))]
    public class AmmoBox : MonoBehaviour
    {

        [Tooltip("Bullet = 0, Grenade and fuel = 1")]
        public AmmoType ammoType;

        [Range(0, 50)]
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
    
    public enum AmmoType
    {
        AmmoType0 = 0, AmmoType1
    } 
}
