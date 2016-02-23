using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GalacticJanitor.UI
{

    public class PlayerStateDisplay : EntityResourceDisplay
    {

        public AmmoDisplay ammo1;
        public AmmoDisplay ammo2;

        [Header("Entrave Display")]
        public GameObject m_entraveDisplay;
        public ProgressBar m_entraveBar;

        public void DisplayInfoWeapon1(int ammoCarried, int ammoInMagazine)
        {
            ammo1.UpdateText(ammoInMagazine, ammoCarried);
        }

        public void DisplayInfoWeapon2(int ammoCarried, int ammoInMagazine)
        {
            ammo2.UpdateText(ammoInMagazine, ammoCarried);
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

        public void DisplayEntrave(bool value)
        {
            m_entraveDisplay.gameObject.SetActive(value);
        }

        public void UpdateEntrave(int remain, int max)
        {
            if (m_entraveDisplay.gameObject.activeInHierarchy)
                m_entraveBar.UpdateProgress(remain, max);
        }
    }
}
