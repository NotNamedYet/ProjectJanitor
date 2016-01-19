using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    /// <summary>
    /// Inventory ammo, handle also collision with AmmoBox
    /// </summary>
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
        private void PickUp(AmmoType ammoType, int amount)
        {
            if (ammoType == AmmoType.AmmoType0)
                ammoCarriedType0 += amount;
            else
                ammoCarriedType1 += amount;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ammo Box")
            {
                Debug.Log("I touch something and i like it : it was an ammoBox");

                AmmoBox box = other.gameObject.GetComponent<AmmoBox>();
                PickUp(box.ammoType, box.amount);
                box.amount = 0; 
            }
        }
    } 
}
