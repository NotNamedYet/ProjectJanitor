using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    // TODO : Maybe put a little time of waiting between shoot one by one and rafale
    public class WeaponControllerHartman : MonoBehaviour
    {
        public MarinesType marinesType;

        public WeaponAssaultRifle assaultRifle;
        public PlayerAmmo playerAmmo;

        public bool playerCanShoot = true;
        public float timerActive = 0f; // Timer in real time
        public float timer = 0.5f; // Timer set

        // Use this for initialization
        void Start()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            assaultRifle = gameObject.GetComponent<WeaponAssaultRifle>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateReloadTimer();

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (playerCanShoot)
                {
                    assaultRifle.ReloadMagazine();
                    playerCanShoot = false; 
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // One left click
            {
                if (playerCanShoot)
                    assaultRifle.Fire();

            }

            if (Input.GetKey(KeyCode.Mouse0))  // Hold click
            {
                if (playerCanShoot)
                    assaultRifle.ConstantFire();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) // Release click
            {
                assaultRifle.ReleaseTrigger();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1)) // One right click
            {
                if (playerCanShoot)
                    assaultRifle.FireGrenade();
            }
        }

        /// <summary>
        /// Use to prevent player to shoot after reloading or something else.
        /// </summary>
        void UpdateReloadTimer()
        {
            if (!playerCanShoot)
            {
                timerActive += Time.deltaTime;
                if (timerActive >= timer)
                {
                    timerActive = 0;
                    playerCanShoot = true;
                }
            }
        }

    }

}