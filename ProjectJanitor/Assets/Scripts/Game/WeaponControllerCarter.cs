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

        [Range(0, 1)]
        public int indexActiveWeapon; // 0 = Double guns ; 1 = Flamethrower

        [HideInInspector]
        public bool playerCanShootAfterReload = true;

        float timerCancelFireAfterReloadActive = 0f; // Timer in real time

        [Tooltip("Time before the player can shoot again after reloaded")]
        public float timerCancelFireAfterReload = 2f; // Timer set. TODO : match with the sound's time of reloading

        // Use this for initialization
        void Start()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            doubleGuns = gameObject.GetComponent<WeaponDoubleGuns>();
            flamethrower = gameObject.GetComponent<WeaponFlamethrower>();

            StartWeaponDoubleGuns();  // TODO : Change this maybe with savegames
        }

        // Update is called once per frame
        void Update()
        {

            UpdateReloadTimer();

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (playerCanShootAfterReload)
                {
                    SwitchIndexWeapon();
                    playerCanShootAfterReload = false; // TODO : Put it or not ? Game design choice
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (playerCanShootAfterReload)
                {
                    Reload();
                    playerCanShootAfterReload = false; 
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // One click
            {
                if (playerCanShootAfterReload)
                {
                    if (indexActiveWeapon == 0)
                        doubleGuns.Fire();
                    else
                        flamethrower.Fire(); 
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

        void SwitchIndexWeapon()
        {
            indexActiveWeapon = indexActiveWeapon == 0 ? 1 : 0; // Protect if index != 0 or 1
            // Furvent est content parce qu'il a fait une ternaire, bravo c'est illisible maintenant.
            // J'aime pas les ternaires, c'est naze, c'est un truc pour faire genre "je suis un pgm du code",
            // et franchement, pour Galactic Janitor, ce que l'on gagne en visibilit� on le perds en compr�hension.
            // Sinon j'aime le poulet frit halal. Sign� : Rachid.
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
