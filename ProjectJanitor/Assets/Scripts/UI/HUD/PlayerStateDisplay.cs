using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GalacticJanitor.UI
{

    public class PlayerStateDisplay : EntityResourceDisplay
    {
        public Text ammoType1;
        public Text ammoType2;
        public SpriteRenderer indexWeapon;

        public void DisplayInfoWeapon1(int ammoCarried, int ammoInMagazine)
        {
            if (ammoType1) ammoType1.text = string.Format("{1}/{0}", ammoCarried, ammoInMagazine);
        }

        public void DisplayInfoWeapon2(int ammoCarried, int ammoInMagazine)
        {
            if (ammoType2) ammoType2.text = string.Format("{1}/{0}", ammoCarried, ammoInMagazine);
        }

        /// <summary>
        /// Give info about which weapon Carter is equiped.
        /// Return true if player is Carter, false if Hartman.
        /// Default index for Hartman is 0 (can have only one weapon).
        /// </summary>
        /// <param name="index"> 0 == double guns || 1 == flamethrower </param>
        /// <param name="isCarter"></param>
        /// <returns></returns>
        public bool DisplayInfoIndexWeapon(int index, bool isCarter)
        {
            //if (indexWeapon) 
            return isCarter;
        }

    }
}
