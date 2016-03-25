using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{

    /// <summary>
    /// Inventory ammo, handle also collision with AmmoBox
    /// </summary>
    public class PlayerAmmo : MonoBehaviour
    {

        public int ammoCarriedType0;
        public int ammoCarriedType1;

        public AudioClip sndPickUp;

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
        public void PickUpAmmo(AmmoType ammoType, int amount)
        {
            PlayerController pc = GameController.Player;

            if (ammoType == AmmoType.AmmoType0)
            {
                ammoCarriedType0 += amount;

                if (pc.isCarter())
                {
                    pc.DisplayInfoWeapon1(ammoCarriedType0, pc.weapCCarter.doubleGuns.magazine);
                }
                else
                {
                    pc.DisplayInfoWeapon1(ammoCarriedType0, pc.weapCHartman.assaultRifle.magazineBullet);
                }
            }
            else
            {
                ammoCarriedType1 += amount;

                if (pc.isCarter())
                {
                    pc.DisplayInfoWeapon2(ammoCarriedType1, pc.weapCCarter.flamethrower.magazine);
                }
                else
                {
                    pc.DisplayInfoWeapon2(ammoCarriedType1, pc.weapCHartman.assaultRifle.magazineGrenade);
                }
            }

            GameController.NotifyPlayer("+" + amount + " Ammo !", Color.green, 1);

            /*SOUNDS*/

            if (sndPickUp)
            {
                AudioSource listener = GetComponent<AudioSource>();
                listener.PlayOneShot(sndPickUp, 2.5f);
            }
            
        }

    } 

}
