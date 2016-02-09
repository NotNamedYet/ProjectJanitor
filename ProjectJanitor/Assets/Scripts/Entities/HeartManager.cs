using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{

    public class HeartManager : MonoBehaviour
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
            transform.Rotate(new Vector3(0, 1, 0));

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
                if (target.Heal(amount))
                {
                    Destroy(gameObject);
                    Debug.Log("I was a heart and i healed a marine with " + amount + " hp");
                }

                else
                {
                    Debug.Log("Marine is full life");
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
