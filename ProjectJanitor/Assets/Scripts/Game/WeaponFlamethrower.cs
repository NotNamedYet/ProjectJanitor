using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class WeaponFlamethrower : MonoBehaviour
    {

        PlayerController playerController;
        PlayerAmmo playerAmmo;

        public GameObject jetFlame;
        private GameObject flame;
        private WeaponControllerCarter controller;

        // The three next variables are used to handle the flame consumption of ammo
        public bool flameIsActive = false;
        private float timerActive = 0f; // Timer in real time
        public float timer = 0.1f; // Timer set

        [Range(0, 50)] // Must be equal to magazineSize
        public int magazine;
        public readonly int magazineSize = 50;

        public int flameDmg = 1;

        public Transform chokes; // From where the flame go out the flamethrower

        [Header("Sounds", order = 1)]
        public AudioClip sndReload;
        public AudioClip sndEmpty;

        private AudioSource listener;

        // Use this for initialization
        void Start()
        {
            controller = gameObject.GetComponent<WeaponControllerCarter>();
            playerController = gameObject.GetComponent<PlayerController>();
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            listener = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            FlameHandle();
            CheckIfPlayerCanShoot();
        }

        public bool CheckMagazine()
        {
            if (magazine > 0)
                return true;
            else
                return false; ;
        }

        public void ReloadMagazine()
        {
            if (magazine < magazineSize)
            {
                int ammoNeeded = magazineSize - magazine;

                if (ammoNeeded <= playerAmmo.ammoCarriedType1)// Check if there is enough ammo in player's inventory, type 1 of course
                {
                    magazine += ammoNeeded;
                    playerAmmo.ammoCarriedType1 -= ammoNeeded;
                    listener.PlayOneShot(sndReload);
                }
                else // Not enough ammo in player's inventory
                {
                    if (playerAmmo.ammoCarriedType1 == 0) // No stock of ammo in player's inventory
                    {
                        Debug.Log("Can't reload, out of ammo, i'm in ReloadMagazine of WeaponFlamethrower");
                        // Play empty magazine's sound, something like that
                    }
                    else
                    {
                        magazine += playerAmmo.ammoCarriedType1;
                        playerAmmo.ammoCarriedType1 = 0;
                        listener.PlayOneShot(sndReload);
                    }
                }
            }
            else
            {
                Debug.Log("Magazine is fulled up of ammos, stupid player, i'm in ReloadMagazine() of weaponFlamethrower");
            }
        }

        /// <summary>
        /// Instantiate the flame, pass damage and timer.
        /// </summary>
        public void Fire()
        {
            if (CheckMagazine())
            {
                Debug.Log("i pulled the trigger with the FlameThrower in \"WeaponFlameThrower\"");
                flame = Instantiate(jetFlame, chokes.position, chokes.rotation) as GameObject;
                if (flame == null)
                {
                    Debug.Log("Problem with instantiation of Flame in function Fire in WeaponFlameThrower");
                }
                else
                {
                    flame.transform.parent = chokes.transform; // Attach the GameObject with the effect particle (and maybe collider?) to the weapon
                    flame.GetComponent<FlameController>().flameDmg = this.flameDmg;
                    flame.GetComponent<FlameController>().timer = this.timer;
                    flameIsActive = true;
                }
            }
            else
            {
                Debug.Log("OUT OF AMMO and i'm in function Fire of WeaponFlameThrower");
                if (Input.GetKeyDown(KeyCode.Mouse0)) listener.PlayOneShot(sndEmpty);
            }
        }

        /// <summary>
        /// Destroy the game object flame
        /// </summary>
        public void ReleaseFireKeyFlamethrower()
        {
            Debug.Log("I'm in ReleaseTrigger of WeaponFlameThrower");
            if (flame != null)
            {
                Destroy(flame);
                flameIsActive = false;
            }
        }

        /// <summary>
        /// Destroy flame if no ammo, otherwise consume ammo.
        /// </summary>
        public void FlameHandle()
        {
            if (flameIsActive)
            {
                playerController.justHaveShoot = true; // Use to anim
                playerController.timerActiveJustHaveShoote = 0f;
                if (CheckMagazine())
                {
                    FlameConsumeAmmoTimer();
                }
                else
                {
                    if (flame != null)
                    {
                        Destroy(flame);
                        flameIsActive = false;
                        Debug.Log("OUT OF AMMO in FlameHandle of WeaponFlameThrower");
                    }
                    else
                        Debug.Log("Cry...");

                }
            }
        }

        /// <summary>
        /// Use with the timer to handle munition.
        /// </summary>
        private void FlameConsumeAmmoTimer()
        {
            if (flameIsActive)
            {
                timerActive += Time.deltaTime;
                if (timerActive >= timer)
                {
                    timerActive = 0f;
                    magazine--;
                }
            }
        }

        /// <summary>
        /// Use to stop the flame when the player reload.
        /// </summary>
        public void CheckIfPlayerCanShoot()
        {
            if(!controller.playerCanShootAfterReload)
            {
                Destroy(flame);
                flameIsActive = false;
            }
        }
    } 
}
