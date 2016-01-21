using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class WeaponControllerHartman : MonoBehaviour
    {
        public MarinesType marinesType;

        public WeaponAssaultRifle assaultRifle;
        public PlayerAmmo playerAmmo;

        // Use this for initialization
        void Start()
        {
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            assaultRifle = gameObject.GetComponent<WeaponAssaultRifle>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                assaultRifle.ReloadMagazine();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) // One click
            {
                assaultRifle.Fire();

            }

            if (Input.GetKey(KeyCode.Mouse0))  // Hold click
            {
                assaultRifle.ConstantFire();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) // Release click
            {
                assaultRifle.ReleaseTrigger();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1)) // One right click
            {
                assaultRifle.FireGrenade();
            }
        }
    }

}