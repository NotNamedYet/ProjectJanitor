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

        [Range(0, 300)]
        public int amount = 0;

        [Tooltip("Check it if you want a random number, use min and maxRangeToRandom")]
        public bool useRandomAmount;
        public int minRangeToRandom;
        public int maxRangeToRandom;

        private string uniqueID;

        // Use this for initialization
        void Start()
        {
            if (useRandomAmount) amount = MakeRandomAmount(minRangeToRandom, maxRangeToRandom);
        }

        // Update is called once per frame
        void Update()
        {
            if (amount <= 0)
            {
                Destroy(gameObject);
            }
        }

        public int MakeRandomAmount(int min, int max)
        {
            int result = Random.Range(min - 1, max + 1);
            return result;
        }
    }
    
    public enum AmmoType
    {
        AmmoType0 = 0, AmmoType1
    }
}
