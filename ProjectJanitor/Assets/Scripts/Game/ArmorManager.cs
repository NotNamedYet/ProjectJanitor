using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class ArmorManager : MonoBehaviour
    {

        public int amount;

        [Tooltip("Check it if you want a random number, use min and maxRangeToRandom")]
        public bool useRandomAmount;
        public int minRangeToRandom;
        public int maxRangeToRandom;

        // Use this for initialization
        void Start()
        {
            if (useRandomAmount)
                amount = MakeRandomAmount(minRangeToRandom, maxRangeToRandom);
        }

        // Update is called once per frame
        void Update()
        {
            if (amount <= 0)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && other.GetComponent<LivingEntity>())
            {
                LivingEntity target = other.GetComponent<LivingEntity>();
                if (target.RepairArmor(amount))
                {
                    Destroy(gameObject);
                    Debug.Log("I was an armor and i give to the marine " + amount + " AP");
                }

                else
                {
                    Debug.Log("Marine is full armor");
                }
            }
        }

        /// <summary>
        /// Use to make a random amount, if the flag useRandomAmount is checked.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int MakeRandomAmount(int min, int max)
        {
            int result = Random.Range(min - 1, max + 1);
            return result;
        }
    }

}