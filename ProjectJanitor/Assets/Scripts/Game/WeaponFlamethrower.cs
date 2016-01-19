using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class WeaponFlamethrower : MonoBehaviour
    {
        public GameObject jetFlame;
        private GameObject flame;

        // The three next variables are used to handle the flame consumption of ammo
        public bool flameIsActive = false;
        public float timerActive = 0f; // Timer in real time
        public float timer = 0.1f; // Timer set

        [Range(0, 300)] // Must be equal to magazineSize
        public int magazine;
        public readonly int magazineSize = 300;

        public Transform chokes; // From where the flame go out the flamethrower

        public PlayerAmmo playerAmmo;

        // Use this for initialization
        void Start()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
        }

        // Update is called once per frame
        void Update()
        {
            FlameHandle();
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
                    }
                }
            }
        }

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
                    flameIsActive = true;
                }
            }
            else
                Debug.Log("OUT OF AMMO and i'm in function Fire of WeaponFlameThrower");
        }

        public void ReleaseFireKeyFlamethrower()
        {
            Debug.Log("I'm in ReleaseTrigger of WeaponFlameThrower");
            if (flame != null)
            {
                Destroy(flame);
                flameIsActive = false;
            }
        }

        public void FlameHandle()
        {
            if (flameIsActive)
            {
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
    } 
}
