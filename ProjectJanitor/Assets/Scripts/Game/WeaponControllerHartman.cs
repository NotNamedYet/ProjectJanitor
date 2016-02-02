using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{

    // TODO : Maybe put a little time of waiting between shoot one by one and rafale
    public class WeaponControllerHartman : MonoBehaviour
    {

        public WeaponAssaultRifle assaultRifle;
        public PlayerAmmo playerAmmo;
        PlayerController playerController;

        [HideInInspector]
        public bool playerCanShootAfterReload = true;

        float timerCancelFireAfterReloadActive = 0f; // Timer in real time

        [Tooltip("Time before the player can shoot again after reloaded")]
        public float timerCancelFireAfterReload = 0.5f; // Timer set

        // Use this for initialization
        void Start()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            assaultRifle = gameObject.GetComponent<WeaponAssaultRifle>();
            playerController = gameObject.GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateReloadTimer();

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (playerCanShootAfterReload)
                {
                    assaultRifle.ReloadMagazine();
                    playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, assaultRifle.magazineBullet);
                    playerController.DisplayInfoWeapon2(playerAmmo.ammoCarriedType1, assaultRifle.magazineGrenade);
                    playerCanShootAfterReload = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // One left click
            {
                if (playerCanShootAfterReload)
                {
                    assaultRifle.Fire();
                    playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, assaultRifle.magazineBullet);
                }

            }

            if (Input.GetKey(KeyCode.Mouse0))  // Hold click
            {
                if (playerCanShootAfterReload)
                {
                    assaultRifle.ConstantFire();
                    playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, assaultRifle.magazineBullet);
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) // Release click
            {
                assaultRifle.ReleaseTrigger();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1)) // One right click
            {
                if (playerCanShootAfterReload)
                {
                    assaultRifle.FireGrenade();
                    playerController.DisplayInfoWeapon2(playerAmmo.ammoCarriedType1, assaultRifle.magazineGrenade);
                }
            }
        }

        /// <summary>
        /// Use to prevent player to shoot after reloading or something else.
        /// </summary>
        void UpdateReloadTimer()
        {
            if (!playerCanShootAfterReload)
            {
                timerCancelFireAfterReloadActive += Time.deltaTime;
                if (timerCancelFireAfterReloadActive >= timerCancelFireAfterReload)
                {
                    timerCancelFireAfterReloadActive = 0;
                    playerCanShootAfterReload = true;
                }
            }
        }

    }

}