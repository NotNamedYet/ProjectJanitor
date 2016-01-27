using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{

    // TODO : Maybe put a little time of waiting between shoot one by one and rafale
    public class WeaponControllerHartman : MonoBehaviour
    {

        public WeaponAssaultRifle assaultRifle;
        public PlayerAmmo playerAmmo;

        [HideInInspector]
        public bool playerCanShootAfterReload = true;

        float timerCancelFireAfterReloadActive = 0f; // Timer in real time

        [Tooltip("Time before the player can shoot again after reloaded")]
        public float timerCancelFireAfterReload = 0.5f; // Timer set

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
                if (playerCanShootAfterReload)
                {
                    assaultRifle.ReloadMagazine();
                    playerCanShootAfterReload = false; 
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // One left click
            {
                if (playerCanShootAfterReload)
                    assaultRifle.Fire();

            }

            if (Input.GetKey(KeyCode.Mouse0))  // Hold click
            {
                if (playerCanShootAfterReload)
                    assaultRifle.ConstantFire();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) // Release click
            {
                assaultRifle.ReleaseTrigger();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1)) // One right click
            {
                if (playerCanShootAfterReload)
                    assaultRifle.FireGrenade();
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