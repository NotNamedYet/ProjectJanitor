using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GalacticJanitor.UI
{
    public class PlayerStateDisplay : EntityResourceDisplay
    {
        public Text ammoType1;
        public Text ammoType2;

        public void DisplayInfoWeapon1(int ammoCarried, int ammoInMagazine)
        {
            if (ammoType1) ammoType1.text = string.Format("{1}/{0}", ammoCarried, ammoInMagazine);
        }

        public void DisplayInfoWeapon2(int ammoCarried, int ammoInMagazine)
        {
            if (ammoType2) ammoType2.text = string.Format("{1}/{0}", ammoCarried, ammoInMagazine);
        }


    }
}
