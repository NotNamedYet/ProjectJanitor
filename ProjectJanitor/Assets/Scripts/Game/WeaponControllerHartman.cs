using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

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

        void Awake()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            assaultRifle = gameObject.GetComponent<WeaponAssaultRifle>();
            playerController = gameObject.GetComponent<PlayerController>();
        }

        // Use this for initialization
        void Start()
        {
            RefreshAmmoHUD();
        }

        public void RefreshAmmoHUD()
        {
            /* GUI */
            if (playerController.playerDisplay)
            {
                playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, assaultRifle.magazineBullet);
                playerController.DisplayInfoWeapon2(playerAmmo.ammoCarriedType1, assaultRifle.magazineGrenade);
                playerController.playerDisplay.ammo1.DisplayBullet();
                playerController.playerDisplay.ammo2.DisplayGrenade();
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdateReloadTimer();
			
            if (!GameController.PausedGame) UpdateInput();
        }

        void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (playerCanShootAfterReload)
                {
                    assaultRifle.ReloadMagazine();
                    playerCanShootAfterReload = false;

                    /*GUI*/
                    playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, assaultRifle.magazineBullet);
                    playerController.DisplayInfoWeapon2(playerAmmo.ammoCarriedType1, assaultRifle.magazineGrenade);
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // One left click
            {
                if (playerCanShootAfterReload)
                {
                    assaultRifle.Fire();

                    /*GUI*/
                    playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, assaultRifle.magazineBullet);
                }

            }

            if (Input.GetKey(KeyCode.Mouse0))  // Hold click
            {
                if (playerCanShootAfterReload)
                {
                    assaultRifle.ConstantFire();

                    /*GUI*/
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

                        /*GUI*/
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