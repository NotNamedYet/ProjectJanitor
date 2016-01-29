using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class WeaponAssaultRifle : MonoBehaviour
    {

        PlayerController playerController;
        PlayerAmmo playerAmmo;

        public GameObject projectileBullet;
        public GameObject projectileGrenade;

        [Range(0, 75)]
        public int magazineBullet;
        public readonly int magazineSizeBullet = 75;

        [Range(0, 1)]
        public int magazineGrenade;
        public readonly int magazineSizeGrenade = 1;

        public int bulletsDmg = 1;
        public int grenadesDmg = 2;

        public Transform chokes; // From where the bullet go out the gun

        [Header("Burst deployment", order = 0)]
        [Tooltip("Time during player must hold clic before burst mode is active")]
        public float canBurstTimer = 0.2f;

        //[HideInInspector]
        public float canBurstTimerActive = 0;

        public bool burstModeIsActive = false;

        [Header("Burst configuration", order = 1)]
        [Tooltip("Time between two bullets.")]
        public float nextBulletTimerBase = 0.1f; // Base timer

        [Tooltip("Amplitude use on time between two bullets. Set to zero if you don't want to.")]
        public float nextBulletTimerAmplitude = 0.1f; // Use to make a random effect with the timer

        private float nextBulletTimerActive = 0f; // Timer in real time
        private float nextBulletTimer; // Timer that can me modifie with nextBulletTimerAmplitude, see the function LaunchTimer()
        private bool canConstantFireNextBullet = true;

        // Use this for initialization
        void Start()
        {
            nextBulletTimer = nextBulletTimerBase;
            playerController = gameObject.GetComponent<PlayerController>();
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
        }

        // Update is called once per frame
        void Update()
        {
            LaunchTimer();
            //BurstActivation();
        }

        /// <summary>
        /// Handle the timer with the flag canConstantFireNextBullet
        /// </summary>
        private void LaunchTimer()
        {
            if (!canConstantFireNextBullet)
            {
                nextBulletTimerActive += Time.deltaTime;
                if (nextBulletTimerActive >= nextBulletTimer)
                {
                    nextBulletTimerActive = 0f;
                    canConstantFireNextBullet = true;
                    nextBulletTimer = Random.Range(nextBulletTimerBase - nextBulletTimerAmplitude, nextBulletTimerBase + nextBulletTimerAmplitude);
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
            if (canConstantFireNextBullet && burstModeIsActive)
                Fire();
            BurstActivation();
        }

        public void ReleaseTrigger()
        {
            burstModeIsActive = false;
            canBurstTimerActive = 0f;

            canConstantFireNextBullet = true;
            nextBulletTimerActive = 0f;
        }

        public void Fire()
        {
            if (CheckMagazineBullet())
            {
                playerController.justHaveShoot = true; // Launch the fire animation
                Invoke("InvokeBullet", 0.05f)
;            }

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
                playerController.justHaveShoot = true; // Launch the fire animation
                Invoke("InvokeGrenade", 0.05f);
            }
            else
            {
                Debug.Log("OUT OF AMMO and i'm in function AlternateFire (GRENADE) of WeaponAssaultRifle");
                // Play empty magazine sound
            }
        }

        void InvokeGrenade()
        {
            Debug.Log("i pulled the trigger with the GRENADEAssaultRifle in \"WeaponAssaultRifle\"");
            GameObject grenade = Instantiate(projectileGrenade, chokes.position, chokes.rotation) as GameObject;
            GrenadeController gctrl = grenade.GetComponent<GrenadeController>();
            gctrl.grenadeDmg = grenadesDmg;
            gctrl.SetSource(gameObject); // Use to assign the marine as target to the alien
            magazineGrenade--;
            playerController.timerActiveJustHaveShoote = 0f; // Use to animation, see PlayerController

            //Play a nice badass sound
        }

        void InvokeBullet()
        {
            Debug.Log("i pulled the trigger with the AssaultRifle in \"WeaponAssaultRifle\"");
            GameObject bullet = Instantiate(projectileBullet, chokes.position, chokes.rotation) as GameObject;
            BulletController bctrl = bullet.GetComponent<BulletController>();
            bctrl.bulletDmg = bulletsDmg;
            bctrl.SetSource(gameObject); // Use to assign the marine as target to the alien
            magazineBullet--;
            canConstantFireNextBullet = false;
            playerController.timerActiveJustHaveShoote = 0f; // Use to animation, see PlayerController

            //Play a nice badass sound
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
                Debug.Log("Magazine is fulled up of ammos, stupid player, i'm in ReloadBullet() of WeaponAssaultRifle");
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
                Debug.Log("Magazine is fulled up of ammos, stupid player, i'm in ReloadGrenade() of WeaponAssaultRifle");
        }

        /// <summary>
        /// When player begin to hold clic to fire, launch the timer. Call the anim to keep the fire animation.
        /// Is update ONLY when left clic is hold, in ConstantFire().
        /// </summary>
        void BurstActivation()
        {
            if (!burstModeIsActive)
            {
                canBurstTimerActive += Time.deltaTime;
                if (canBurstTimerActive >= canBurstTimer)
                {
                    burstModeIsActive = true;
                    canBurstTimerActive = 0f;
                }
            }
        }
    } 
}
