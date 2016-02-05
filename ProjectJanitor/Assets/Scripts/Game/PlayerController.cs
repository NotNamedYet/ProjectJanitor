using UnityEngine;
using System.Collections;
using GalacticJanitor.UI;
using GalacticJanitor.Persistency;
using GalacticJanitor.Engine;

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
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {

        public MarinesType marinesType;

        public LivingEntity livingEntity;
        public PlayerAmmo playerAmmo;


        public float speed = 10;
        public bool freeze;

        public bool isPause;

        Rigidbody body;
        public PlayerRotation rotate; // Ref to the gameObject that must rotate, with the script PlayerRotation

        public Animator anim;
        [HideInInspector]
        public bool justHaveShoot = false;
        [HideInInspector]
        public float timerActiveJustHaveShoote = 0; // public, need to be accessible in weapon's script, but must be hide in inspector
        [Tooltip("Timer handle fire animation, time before switch to still or move animations. 0.5f to Hartman, 0.1f to Carter")]
        public float timerJustHaveShoot = 0.5f; // 0.5f to Hartman, 0.1f to Carter

        [Header("GUI")]
        public PlayerStateDisplay display;

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
                if (GetComponent<WeaponControllerCarter>().IndexActiveWeapon == 1 && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0)) // If Carter is equiped with flamethrower
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
            /*
                        if (display)
                        {
                            display.ammoCarriedType1 = ammoCarried;
                            display.ammoInMagazineWeapon1 = ammoInMagazine; 
                        }
            */
            if (display) display.DisplayInfoWeapon1(ammoCarried, ammoInMagazine);

        }

        public void DisplayInfoWeapon2(int ammoCarried, int ammoInMagazine)
        {
            /* display.ammoCarriedType2 = playerAmmo.ammoCarriedType1;

             if (marinesType == MarinesType.MajCarter)
                 display.ammoInMagazineWeapon2 = gameObject.GetComponent<WeaponControllerCarter>().flamethrower.magazine;

             else
                 display.ammoInMagazineWeapon2 = gameObject.GetComponent<WeaponControllerHartman>().assaultRifle.magazineGrenade;*/
            /*
            if (display)
            {
                display.ammoCarriedType2 = ammoCarried;
                display.ammoInMagazineWeapon2 = ammoInMagazine; 
            }
            */
            if (display) display.DisplayInfoWeapon2(ammoCarried, ammoInMagazine);
        }

        /// <summary>
        /// Display info about which weapon MajCarter is equiped.
        /// Send false if player is SgtHartman.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void DisplayInfoIndexWeapon(int index)
        {
            if (marinesType == MarinesType.MajCarter)
            {
                if (display) display.DisplayInfoIndexWeapon(index, true);
            }

            else display.DisplayInfoIndexWeapon(0, false);
        }

        void LoadPlayerData()
        {
            if (GameController.Controller == null)
                return;

            PlayerData data = GameController.Controller.Registery.playerData;

            playerAmmo.ammoCarriedType0 = data.inventoryAmmo0;
            playerAmmo.ammoCarriedType1 = data.inventoryAmmo1;

            if (marinesType == MarinesType.MajCarter)
            {
                WeaponControllerCarter wpc = GetComponent<WeaponControllerCarter>();
                wpc.IndexActiveWeapon = data.weaponIndex;
                wpc.doubleGuns.magazine = data.magazineAmmo0;
                wpc.flamethrower.magazine = data.magazineAmmo1;
            }
            else
            {
                WeaponControllerHartman wph = GetComponent<WeaponControllerHartman>();
                wph.assaultRifle.magazineBullet = data.magazineAmmo0;
                wph.assaultRifle.magazineGrenade = data.magazineAmmo1;
            }

            livingEntity.health = data.playerHealth;
            livingEntity.maxHealth = data.playerMaxHealth;
            livingEntity.armorPoint = data.playerArmor;
            livingEntity.maxArmorPoint = data.playerMaxArmor;
        }

        public PlayerData GetPlayerData()
        {
            PlayerData data = new PlayerData();

            data.playerType = marinesType;
            data.inventoryAmmo0 = playerAmmo.ammoCarriedType0;
            data.inventoryAmmo1 = playerAmmo.ammoCarriedType1;

            if (marinesType == MarinesType.MajCarter)
            {
                WeaponControllerCarter wpc = GetComponent<WeaponControllerCarter>();
                data.weaponIndex = wpc.IndexActiveWeapon;
                data.magazineAmmo0 = wpc.doubleGuns.magazine;
                data.magazineAmmo1 = wpc.flamethrower.magazine;
            }
            else
            {
                WeaponControllerHartman wph = GetComponent<WeaponControllerHartman>();
                data.magazineAmmo0 = wph.assaultRifle.magazineBullet;
                data.magazineAmmo1 = wph.assaultRifle.magazineGrenade;
            }

            data.playerHealth = livingEntity.health;
            data.playerMaxHealth = livingEntity.maxHealth;
            data.playerArmor = livingEntity.armorPoint;
            data.playerMaxArmor = livingEntity.maxArmorPoint;

            return data;
        }
        
        public void UpdateData()
        {
            GameController.Controller.Registery.playerData = GetPlayerData();
        } 
    }

    public enum MarinesType
    {
        MajCarter, SgtHartman
    }
}