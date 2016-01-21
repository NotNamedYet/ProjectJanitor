using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class WeaponControllerCarter : MonoBehaviour
    {
        public MarinesType marinesType;

        public GameObject[] rigWeaponAndArms; // 0 = Double guns ; 1 = Flamethrower
        public WeaponDoubleGuns doubleGuns;
        public WeaponFlamethrower flamethrower;
        public int indexActiveWeapon; // 0 = Double guns ; 1 = Flamethrower

        public PlayerAmmo playerAmmo;

        // Use this for initialization
        void Start()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            doubleGuns = gameObject.GetComponent<WeaponDoubleGuns>();
            flamethrower = gameObject.GetComponent<WeaponFlamethrower>();

            if (rigWeaponAndArms != null && rigWeaponAndArms.Length > 1)
            {
                //Use the double gun at start
                indexActiveWeapon = 0;
                rigWeaponAndArms[0].SetActive(true);
                rigWeaponAndArms[1].SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SwitchWeapon();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // One click
            {
                if (indexActiveWeapon == 0)
                    doubleGuns.Fire();
                else
                    flamethrower.Fire();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) // Release click
            {
                if (indexActiveWeapon == 1)
                {
                    flamethrower.ReleaseFireKeyFlamethrower(); 
                }
            }
        }

        public void SwitchWeapon()
        {
            if (rigWeaponAndArms[0].activeSelf) // If double guns equiped
            {
                rigWeaponAndArms[0].SetActive(false);
                rigWeaponAndArms[1].SetActive(true);
                indexActiveWeapon = 1;
            }
            else // If flamethrower equiped
            {
                rigWeaponAndArms[0].SetActive(true);
                rigWeaponAndArms[1].SetActive(false);
                indexActiveWeapon = 0;
            }
        }

        public void Reload()
        {
            if (indexActiveWeapon == 0) // Reload double guns
                doubleGuns.ReloadMagazine();
            else // Flamethrower
                flamethrower.ReloadMagazine();
        }
 
    }

    public enum MarinesType
    {
        MajCarter, SgtHartman
    }
}
