using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{

    public class WeaponControllerCarter : MonoBehaviour
    {

        public GameObject[] rigWeaponAndArms; // 0 = Double guns ; 1 = Flamethrower
        public WeaponDoubleGuns doubleGuns;
        public WeaponFlamethrower flamethrower;
        public PlayerAmmo playerAmmo;
        PlayerController playerController;

        private int indexActiveWeapon; // 0 = Double guns ; 1 = Flamethrower
        public int IndexActiveWeapon
        {
            get { return indexActiveWeapon; }
            set 
            {
                if (value == 1) indexActiveWeapon = 1;
                else indexActiveWeapon = 0;
            }
        }

        [HideInInspector]
        public bool playerCanShootAfterReload = true;

        float timerCancelFireAfterReloadActive = 0f; // Timer in real time

        [Tooltip("Time before the player can shoot again after reloading")]
        public float timerCancelFireAfterReload = 0.5f; // Timer set. TODO : match with the sound's time of reloading

        void Awake()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            doubleGuns = gameObject.GetComponent<WeaponDoubleGuns>();
            flamethrower = gameObject.GetComponent<WeaponFlamethrower>();
            playerController = gameObject.GetComponent<PlayerController>();
        }

        // Use this for initialization
        void Start()
        {
            StartWeaponDoubleGuns();  // TODO : Change this maybe with savegames

            /*GUI*/
            playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, doubleGuns.magazine);
            playerController.DisplayInfoWeapon2(playerAmmo.ammoCarriedType1, flamethrower.magazine);
            playerController.DisplayInfoIndexWeapon(indexActiveWeapon);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateReloadTimer();
            if (!GalacticJanitor.Engine.GameController.Controller.isInPause) UpdateInput();
        }

<<<<<<< HEAD
        void UpdateInput()
        {
				if (Input.GetKeyDown(KeyCode.F))
                {
                    if (playerCanShootAfterReload)
                    {
                        SwitchIndexWeapon();
                        playerCanShootAfterReload = false; // TODO : Put it or not ? Game design choice

                        /*GUI*/
                        playerController.DisplayInfoIndexWeapon(indexActiveWeapon);
                    }
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (playerCanShootAfterReload)
                    {
                        Reload();
                        playerCanShootAfterReload = false;

                        /*GUI*/
                        if (indexActiveWeapon == 0)

                            playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, doubleGuns.magazine);

                        else

                            playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType1, flamethrower.magazine);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Mouse0)) // One click
                {
                    if (playerCanShootAfterReload)
                    {
                        if (indexActiveWeapon == 0)
                        {
                            doubleGuns.Fire();

                            /*GUI*/
                            playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType0, doubleGuns.magazine);
                        }

                        else
                        {
                            flamethrower.Fire();

                            /*GUI*/
                            playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType1, flamethrower.magazine);
                        }
                    }
                }

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (playerCanShootAfterReload)
                    {
                        if (indexActiveWeapon == 1)
                        {
                            if (!flamethrower.flameIsActive)
                            {
                                flamethrower.Fire();
                            }

                            /*GUI*/
                            playerController.DisplayInfoWeapon1(playerAmmo.ammoCarriedType1, flamethrower.magazine);
                        }
                    }
                }

                if (Input.GetKeyUp(KeyCode.Mouse0)) // Release click
                {
                    if (indexActiveWeapon == 1)
                    {
                        flamethrower.ReleaseFireKeyFlamethrower();
                    }
                } 
		}
        
>>>>>>> Add bool to handle pause
        /// <summary>
        /// Just call the reload magazine of both weapon.
        /// </summary>
        public void Reload()
        {
            if (indexActiveWeapon == 0) // Reload double guns
                doubleGuns.ReloadMagazine();
            else // Flamethrower
                flamethrower.ReloadMagazine();
        }

        /// <summary>
        /// Use to the first game.
        /// </summary>
        public void StartWeaponDoubleGuns()
        {
            indexActiveWeapon = 0;
            UpdateWeapon();
        }

        /// <summary>
        /// Check index and active game object with weapon (and chokes) in consequence.
        /// </summary>
        public void UpdateWeapon()
        {
            if (indexActiveWeapon == 0) // If double guns must be equiped
            {
                rigWeaponAndArms[0].SetActive(true);
                rigWeaponAndArms[1].SetActive(false);
            }
            else // If flamethrower must be equiped
            {
                rigWeaponAndArms[0].SetActive(false);
                rigWeaponAndArms[1].SetActive(true);
            }
        }

        public void SwitchIndexWeapon()
        {
            indexActiveWeapon = indexActiveWeapon == 0 ? 1 : 0; // Protect if index != 0 or 1
            UpdateWeapon();
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
