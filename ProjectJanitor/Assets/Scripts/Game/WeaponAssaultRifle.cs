using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class WeaponAssaultRifle : MonoBehaviour
    {
        public PlayerAmmo playerAmmo;
        public GameObject projectileBullet;
        public GameObject projectileGrenade;

        [Range(0, 75)]
        public int magazineBullet;
        public readonly int magazineSizeBullet = 75;

        [Range(0, 1)]
        public int magazineGrenade;
        public readonly int magazineSizeGrenade = 1;

        public Transform chokes; // From where the bullet go out the gun

        public float timerActive = 0f; // Timer in real time
        public float timer = 0.5f; // Timer set, changes in the function LaunchTimer()
        public float timerBase = 0.5f; // Base timer
        public float timerAmplitude = 0.1f; // Use to make a random effect with the timer
        private bool canConstantFireNextBullet = true;

        // Use this for initialization
        void Start()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
        }

        // Update is called once per frame
        void Update()
        {
            LaunchTimer();
        }

        /// <summary>
        /// Handle the timer with the flag canConstantFireNextBullet
        /// </summary>
        private void LaunchTimer()
        {
            if (!canConstantFireNextBullet)
            {
                timerActive += Time.deltaTime;
                if (timerActive >= timer)
                {
                    timerActive = 0f;
                    canConstantFireNextBullet = true;
                    timer = Random.Range(timerBase - timerAmplitude, timerBase + timerAmplitude);
                }
            }
        }

        public bool CheckMagazineBullet()
        {
            if (magazineBullet > 0)
                return true;
            else
                return false;
        }

        private bool CheckMagazineGrenade()
        {
            if (magazineGrenade > 0)
                return true;
            else
                return false;
        }

        public void ConstantFire()
        {
            if (canConstantFireNextBullet)
                Fire();
        }

        public void ReleaseTrigger()
        {
            canConstantFireNextBullet = true;
            timerActive = 0f;
        }

        public void Fire()
        {
            if (CheckMagazineBullet())
            {
                Debug.Log("i pulled the trigger with the AssaultRifle in \"WeaponAssaultRifle\"");
                Instantiate(projectileBullet, chokes.position, chokes.rotation);
                magazineBullet--;
                canConstantFireNextBullet = false;
                //Play a nice badass sound
            }
            else
            {
                Debug.Log("OUT OF AMMO and i'm in function Fire of WeaponAssaultRifle");
                // Play empty magazine sound
            }
        }

        public void FireGrenade()
        {
            if (CheckMagazineGrenade())
            {
                Debug.Log("i pulled the trigger with the GRENADEAssaultRifle in \"WeaponAssaultRifle\"");
                Instantiate(projectileGrenade, chokes.position, chokes.rotation);
                magazineGrenade--;
                //Play a nice badass sound
            }
            else
            {
                Debug.Log("OUT OF AMMO and i'm in function AlternateFire (GRENADE) of WeaponAssaultRifle");
                // Play empty magazine sound
            }
        }

        public void ReloadMagazine()
        {
            ReloadBullet();
            ReloadGrenade();
        }

        private void ReloadBullet()
        {
            if (magazineBullet < magazineSizeBullet)
            {
                int ammoNeeded = magazineSizeBullet - magazineBullet;

                if (ammoNeeded <= playerAmmo.ammoCarriedType0)// Check if there is enough ammo in player's inventory, type 1 of course
                {
                    magazineBullet += ammoNeeded;
                    playerAmmo.ammoCarriedType0 -= ammoNeeded;
                }
                else // Not enough ammo in player's inventory
                {
                    if (playerAmmo.ammoCarriedType0 == 0) // No stock of ammo in player's inventory
                    {
                        Debug.Log("Can't reload, out of ammo, i'm in ReloadBullet of WeaponAssaultRifle");
                        // Play empty magazine's sound, something like that
                    }
                    else
                    {
                        magazineBullet += playerAmmo.ammoCarriedType0;
                        playerAmmo.ammoCarriedType0 = 0;
                    }
                }
            }
            else
                Debug.Log("Magazine is fulled up of ammo, stupid player, i'm in ReloadBullet() of WeaponAssaultRifle");
        }

        private void ReloadGrenade()
        {
            if (magazineGrenade < magazineSizeGrenade)
            {
                int ammoNeeded = magazineSizeGrenade - magazineGrenade;

                if (ammoNeeded <= playerAmmo.ammoCarriedType1)// Check if there is enough ammo in player's inventory, type 1 of course
                {
                    magazineGrenade += ammoNeeded;
                    playerAmmo.ammoCarriedType1 -= ammoNeeded;
                }
                else // Not enough ammo in player's inventory
                {
                    if (playerAmmo.ammoCarriedType1 == 0) // No stock of ammo in player's inventory
                    {
                        Debug.Log("Can't reload, out of ammo, i'm in ReloadGrenade of WeaponAssaultRifle");
                        // Play empty magazine's sound, something like that
                    }
                    else
                    {
                        magazineGrenade += playerAmmo.ammoCarriedType1;
                        playerAmmo.ammoCarriedType1 = 0;
                    }
                }
            }
            else
                Debug.Log("Magazine is fulled up of ammo, stupid player, i'm in ReloadGrenade() of WeaponAssaultRifle");
        }
    } 
}
