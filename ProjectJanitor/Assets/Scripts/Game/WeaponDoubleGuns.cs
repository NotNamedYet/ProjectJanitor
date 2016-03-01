using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class WeaponDoubleGuns : MonoBehaviour
    {
        public BulletController bullet;

        [Range(0, 30)] // Must be equal to magazineSize
        public int magazine;
        public readonly int magazineSize = 30;
        public int bulletsDmg = 1;

        public Transform chokes1; // From where the bullet go out the gun
        public Transform chokes2;

        private bool activeChokes = true; // True = chokes1, false = chokes2
        [Tooltip("Used by particle named \"pfx_FlashShoot\"")]
        public GameObject flashShoot;

        PlayerAmmo playerAmmo;

        [Header("Sounds", order = 1)]
        public AudioClip[] sndFire;
        public AudioClip sndEmpty;
        public AudioClip sndReload;

        private AudioSource listener;

        // Use this for initialization
        void Start()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            listener = GetComponent<AudioSource>();
        }

        public void Fire()
        {
            if (CheckMagazine())
            {
                BulletController bul = null;

                if (activeChokes)
                {
                    Debug.Log("i pulled the trigger with the left gun in \"WeaponDoubleGun\"");
                    bul = Instantiate(bullet, chokes1.position, chokes1.rotation) as BulletController;
                    PlayFlashShoot(chokes1);


                }
                else
                {
                    Debug.Log("i pulled the trigger with the right gun in \"WeaponDoubleGun\"");
                    bul = Instantiate(bullet, chokes2.position, chokes2.rotation) as BulletController;
                    PlayFlashShoot(chokes2);

                }

                bul.baseDamage = bulletsDmg;
                bul.SetSource(gameObject);
                magazine--;
                listener.PlayOneShot(sndFire[Random.Range(0, sndFire.Length)]);

                activeChokes = !activeChokes;
            }
            else
            {
                Debug.Log("OUT OF AMMO and i'm in function Fire of WeaponDoubleGun");
                if (Input.GetKeyDown(KeyCode.Mouse0)) listener.PlayOneShot(sndEmpty);
            }
        }

        void PlayFlashShoot(Transform chokes)
        {
            if (flashShoot != null)
            {
                GameObject flash = Instantiate(flashShoot, chokes.position, chokes.rotation) as GameObject;
                flash.transform.SetParent(chokes.transform);
                Destroy(flash, 0.11f);
            }
        }

        public bool CheckMagazine()
        {
            if (magazine > 0)
                return true;
            else
                return false;
        }

        public void ReloadMagazine()
        {
            if (magazine < magazineSize)
            {
                int ammoNeeded = magazineSize - magazine;

                if (ammoNeeded <= playerAmmo.ammoCarriedType0)// Check if there is enough ammo in player's inventory, type 0 of course
                {
                    magazine += ammoNeeded;
                    playerAmmo.ammoCarriedType0 -= ammoNeeded;
                    listener.PlayOneShot(sndReload);
                }
                else // Not enough ammo in player's inventory
                {
                    if (playerAmmo.ammoCarriedType0 == 0) // No stock of ammo in player's inventory
                    {
                        Debug.Log("Can't reload, out of ammo, i'm in ReloadMagazine of WeaponDoubleGun");
                        // Play empty magazine's sound, something like that
                    }
                    else
                    {
                        magazine += playerAmmo.ammoCarriedType0;
                        playerAmmo.ammoCarriedType0 = 0;
                        listener.PlayOneShot(sndReload);
                    }
                }
            }
            else
                Debug.Log("Magazine is fulled up of ammos, stupid player, i'm in ReloadMagazine() of WeaponDoubleGun");
        }
    } 
}
