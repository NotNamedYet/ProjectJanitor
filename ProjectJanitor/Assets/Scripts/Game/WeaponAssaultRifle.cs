using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class WeaponAssaultRifle : MonoBehaviour
    {

        #region Fields
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
        [Tooltip("Used by particle named \"pfx_FlashShoot\"")]
        public GameObject flashShoot;

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

        public float nextBulletTimerActive = 0f; // Timer in real time
        public float nextBulletTimer; // Timer that can me modified with nextBulletTimerAmplitude, see the function LaunchTimer()
        public bool canConstantFireNextBullet = true;

        [Header("Sounds", order = 2)]
        public AudioClip[] sndFireBullet;
        public AudioClip sndFireGrenade;

        public AudioClip sndBulletReload;
        public AudioClip sndGrenadeReload;

        public AudioClip sndBulletEmpty;
        public AudioClip sndGrenadeEmpty;

        private AudioSource listener;
        #endregion

        // Use this for initialization
        void Start()
        {
            nextBulletTimer = nextBulletTimerBase;
            playerController = gameObject.GetComponent<PlayerController>();
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            listener = gameObject.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            LaunchTimer();
            //LaunchTimerConstant();
            //BurstActivation();
        }

        #region Fire
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

        private void LaunchTimerConstant()
        {
            if (!canConstantFireNextBullet)
            {
                nextBulletTimerActive += Time.deltaTime;
                if (nextBulletTimerActive >= nextBulletTimerBase)
                {
                    nextBulletTimerActive = 0f;
                    canConstantFireNextBullet = true;
                    //nextBulletTimer = Random.Range(nextBulletTimerBase - nextBulletTimerAmplitude, nextBulletTimerBase + nextBulletTimerAmplitude);
                }
            }
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

        public void ConstantFire()
        {
            playerController.justHaveShoot = true; // Launch the fire animation
            if (canConstantFireNextBullet && burstModeIsActive)
            {
                if (CheckMagazineBullet())
                {
                    InvokeBullet();
                }
                else
                    PlaySoundEmptyBullet();
            }

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
            playerController.justHaveShoot = true; // Launch the fire animation

            if (CheckMagazineBullet())
            {
                Invoke("InvokeBullet", 0.1f);
            }

            else
            {
                Debug.Log("OUT OF AMMO and i'm in function Fire of WeaponAssaultRifle");
                PlaySoundEmptyBullet();
            }
        }

        public void FireGrenade()
        {
            playerController.justHaveShoot = true; // Launch the fire animation
            if (CheckMagazineGrenade())
            {
                Invoke("InvokeGrenade", 0.1f);
            }
            else
            {
                Debug.Log("OUT OF AMMO and i'm in function AlternateFire (GRENADE) of WeaponAssaultRifle");
                PlaySoundEmptyGrenade();
            }
        }

        void InvokeGrenade()
        {
            AnimatorClipInfo[] clipInfo = playerController.anim.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length != 0)
            {
                if (clipInfo[0].clip.name == "anm_Hartman_Fire")
                {
                    //Debug.Log("i pulled the trigger with the GRENADEAssaultRifle in \"WeaponAssaultRifle\"");
                    GameObject grenade = Instantiate(projectileGrenade, chokes.position, chokes.rotation) as GameObject;
                    GrenadeController gctrl = grenade.GetComponent<GrenadeController>();
                    gctrl.baseDamage = grenadesDmg;
                    gctrl.SetSource(gameObject); // Use to assign the marine as target to the alien
                    magazineGrenade--;
                    playerController.timerActiveJustHaveShoote = 0f; // Use to animation, see PlayerController
                    PlayFlashShoot();
                    ReloadGrenade();
                    gameObject.GetComponent<WeaponControllerHartman>().playerCanShootAfterReload = false;
                    PlaySoundFireGrenade();
                }
            }
        }

        void InvokeBullet()
        {
            AnimatorClipInfo[] clipInfo = playerController.anim.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length != 0)
            {
                if (clipInfo.Length == 1)
                {
                    if (clipInfo[0].clip.name == "anm_Hartman_Fire" && CheckMagazineBullet()) // Check if the player is in the good animation and re check ammo
                    {
                        //Debug.Log("i pulled the trigger with the AssaultRifle in \"WeaponAssaultRifle\"");
                        GameObject bullet = Instantiate(projectileBullet, chokes.position, chokes.rotation) as GameObject;
                        BulletController bctrl = bullet.GetComponent<BulletController>();
                        bctrl.baseDamage = bulletsDmg;
                        bctrl.SetSource(gameObject); // Use to assign the marine as target to the alien
                        magazineBullet--;
                        canConstantFireNextBullet = false;
                        playerController.timerActiveJustHaveShoote = 0f; // Use to animation, see PlayerController
                        PlayFlashShoot();
                        PlaySoundFireBullet();
                    }
                }
                //Debug.Log("clipinfo lengt : " + clipInfo.Length);
            }
        }

        void PlayFlashShoot()
        {
            if (flashShoot != null)
            {
                GameObject flash = Instantiate(flashShoot, chokes.position, chokes.rotation) as GameObject;
                flash.transform.SetParent(chokes.transform);
                Destroy(flash, 0.11f); 
            }
        }
        #endregion

        #region Reload
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
                    PlaySoundReloadBullet();
                }
                else // Not enough ammo in player's inventory
                {
                    if (playerAmmo.ammoCarriedType0 == 0) // No stock of ammo in player's inventory
                    {
                        Debug.Log("Can't reload, out of ammo, i'm in ReloadBullet of WeaponAssaultRifle");
                    }
                    else
                    {
                        magazineBullet += playerAmmo.ammoCarriedType0;
                        playerAmmo.ammoCarriedType0 = 0;
                        PlaySoundReloadBullet();
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
                    PlaySoundReloadGrenade();
                }
                else // Not enough ammo in player's inventory
                {
                    if (playerAmmo.ammoCarriedType1 == 0) // No stock of ammo in player's inventory
                    {
                        Debug.Log("Can't reload, out of ammo, i'm in ReloadGrenade of WeaponAssaultRifle");
                    }
                    else
                    {
                        magazineGrenade += playerAmmo.ammoCarriedType1;
                        playerAmmo.ammoCarriedType1 = 0;
                        PlaySoundReloadGrenade();
                    }
                }
            }
            else
                Debug.Log("Magazine is fulled up of ammos, stupid player, i'm in ReloadGrenade() of WeaponAssaultRifle");
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
        #endregion

        #region Sounds

        private void PlaySoundFireBullet()
        {
            listener.PlayOneShot(sndFireBullet[Random.Range(0, sndFireBullet.Length)]);
        }

        private void PlaySoundFireGrenade()
        {
            listener.PlayOneShot(sndFireGrenade);
        }

        private void PlaySoundReloadBullet()
        {
            listener.PlayOneShot(sndBulletReload);
        }

        private void PlaySoundReloadGrenade()
        {
            listener.PlayOneShot(sndGrenadeReload);
        }

        private void PlaySoundEmptyBullet()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) // To avoid flood sounds
            {
                listener.PlayOneShot(sndBulletEmpty); 
            }
        }

        private void PlaySoundEmptyGrenade()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1)) // To avoid flood sounds
            {
                listener.PlayOneShot(sndGrenadeEmpty);
            }
            
        }

        #endregion

    }

}
