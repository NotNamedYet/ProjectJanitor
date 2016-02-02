using UnityEngine;
using System.Collections;
using GalacticJanitor.UI;

namespace GalacticJanitor.Game
{

    /*
        For the player, the rigidbody's Interpolation has to be set to "Interpolate".

        Unity Doc : Interpolation allows you to smooth out the effect of running physics at a fixed frame rate.

        By default interpolation is turned off. 
        Commonly rigidbody interpolation is used on the player's character. 
      * Physics is running at discrete timesteps, while graphics is renderered at variable frame rates. 
        This can lead to jittery looking objects, because physics and graphics are not completely in sync. 
        The effect is subtle but often visible on the player character, especially if a camera follows the main character. 
      * It is recommended to turn on interpolation for the main character but disable it for everything else.

    */
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(LivingEntity))]
    [RequireComponent(typeof(PlayerAmmo))]
    [RequireComponent(typeof(PlayerStateDisplay))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {

        public MarinesType marinesType;

        public LivingEntity livingEntity;
        public PlayerAmmo playerAmmo;
        PlayerStateDisplay display;

        public float speed = 10;
        public bool freeze;

        Rigidbody body;
        public PlayerRotation rotate; // Ref to the gameObject that must rotate, with the script PlayerRotation

        public Animator anim;
        [HideInInspector]
        public bool justHaveShoot = false;
        [HideInInspector]
        public float timerActiveJustHaveShoote = 0; // public, need to be accessible in weapon's script, but must be hide in inspector
        [Tooltip("Timer handle fire animation, time before switch to still or move animations. 0.5f to Hartman, 0.1f to Carter")]
        public float timerJustHaveShoot = 0.5f; // 0.5f to Hartman, 0.1f to Carter

        private float _movementCooldown = 0;
        public float MovementCooldown
        {
            get
            {
                return _movementCooldown;
            }
            set
            {
                freeze = value > 0;
                if (freeze) FreezeVelocity();

                _movementCooldown = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            livingEntity = gameObject.GetComponent<LivingEntity>();
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            display = gameObject.GetComponent<PlayerStateDisplay>();
            body = gameObject.GetComponent<Rigidbody>();
        }

        void Update()
        {
            rotate.ForceLookAt();
            UpdateCooldown();
            UpdateTimerPlayerAnimShoot();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!freeze)
            {
                Movement(); 
            }
        }

        void Movement()
        {
            
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            body.AddRelativeForce(move * speed, ForceMode.VelocityChange); // For Forcemode see 2
            MovementAnim();
        }

        void FreezeVelocity()
        {
            body.velocity = Vector3.zero;
        }

        void UpdateCooldown()
        {
            if (_movementCooldown > 0)
            {
                _movementCooldown -= Time.deltaTime;
            }
            freeze = _movementCooldown > 0;
        }

        /// <summary>
        /// When the flag justHaveShoot is active, play the fire animation.
        /// </summary>
        void UpdateTimerPlayerAnimShoot()
        {
            if (justHaveShoot)
            {
                timerActiveJustHaveShoote += Time.deltaTime;
                anim.SetBool("playerShoot", true);
                if (timerActiveJustHaveShoote >= timerJustHaveShoot)
                {
                    timerActiveJustHaveShoote = 0f;
                    justHaveShoot = false;
                    anim.SetBool("playerShoot", false);
                }
            }
        }

        void MovementAnim()
        {
            if (marinesType == MarinesType.MajCarter)
            {
                if (GetComponent<WeaponControllerCarter>().indexActiveWeapon == 1 && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0)) // If Carter is equiped with flamethrower
                {
                    anim.SetBool("playerMove", true);
                }

                else
                    anim.SetBool("playerMove", false);
            }
            
            else // Hartman
            {
                if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0)
                    anim.SetBool("playerMove", true);
                
                else
                    anim.SetBool("playerMove", false);
            }
        }

        public void DisplayInfoWeapon1(int ammoCarried, int ammoInMagazine)
        {
            /*display.ammoCarriedType1 = playerAmmo.ammoCarriedType0;

            if (marinesType == MarinesType.MajCarter)
                display.ammoInMagazineWeapon1 = gameObject.GetComponent<WeaponControllerCarter>().doubleGuns.magazine;

            else
                display.ammoInMagazineWeapon1 = gameObject.GetComponent<WeaponControllerHartman>().assaultRifle.magazineBullet;*/
            display.ammoCarriedType1 = ammoCarried;
            display.ammoInMagazineWeapon1 = ammoInMagazine;

        }

        public void DisplayInfoWeapon2(int ammoCarried, int ammoInMagazine)
        {
            /* display.ammoCarriedType2 = playerAmmo.ammoCarriedType1;

             if (marinesType == MarinesType.MajCarter)
                 display.ammoInMagazineWeapon2 = gameObject.GetComponent<WeaponControllerCarter>().flamethrower.magazine;

             else
                 display.ammoInMagazineWeapon2 = gameObject.GetComponent<WeaponControllerHartman>().assaultRifle.magazineGrenade;*/
            display.ammoCarriedType2 = ammoCarried;
            display.ammoInMagazineWeapon2 = ammoInMagazine;
        }
    }

    public enum MarinesType
    {
        MajCarter, SgtHartman
    }
}