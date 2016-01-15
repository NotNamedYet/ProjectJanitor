using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class PlayerAmmo : MonoBehaviour
    {
        public int ammoCarriedType0;
        public int ammoCarriedType1;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary> Use to check if there is ammo in player's inventory.
        /// Take a int to know what type of ammo the function must check.
        /// 1 to weapon1 and 2 to weapon2.
        /// </summary>
        public bool checkIfThereIsAmmo(int ammoType)
        {
            if (ammoType == 0)
            {
                if (ammoCarriedType0 > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if (ammoCarriedType1 > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Used to refile ammo when ammo box picked up
        /// </summary>
        private void PickUp(string ammoType, int amount)
        {
            if (ammoType == "AmmoType0")
                ammoCarriedType0 += amount;
            else
                ammoCarriedType1 += amount;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "AmmoBox")
            {
                //Debug.Log("I touch something and i like it : it was a ammoBox");

                //AmmoInBox ammo = other.gameObject.GetComponent<AmmoInBox>();
                //PickUp(ammo.ammoType, ammo.amount);
                //Destroy(other.gameObject); // Destroy the ammo box.
            }
        }
    } 
}
