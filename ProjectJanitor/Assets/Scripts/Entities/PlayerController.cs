using UnityEngine;
using System.Collections;
using GalacticJanitor.UI;
using GalacticJanitor.Engine;
using GalacticJanitor.Game;

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
    [RequireComponent(typeof(PlayerAmmo))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : LivingEntity
    {
        [Header("Player Behavior")]
        public MarinesType marinesType;
        public PlayerAmmo playerAmmo;
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

        [Header("GUI")]
        public PlayerStateDisplay display;

        private WeaponControllerCarter weapCCarter;
        private WeaponControllerHartman weapCHartman;


        //SCORE
        [System.Serializable]
        [SerializeField]
        public struct PlayerScore
        {
            public int enemyKilled;
        }
        public PlayerScore score;
        

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

        void Awake()
        {
            anim = gameObject.GetComponent<Animator>();
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            body = gameObject.GetComponent<Rigidbody>();

            if (marinesType == MarinesType.MajCarter)
            { weapCCarter = GetComponent<WeaponControllerCarter>(); }
            else
            { weapCHartman = GetComponent<WeaponControllerHartman>(); }

            GameController.Player = this;
            GameController.TopDownCamera.SetTarget(transform);
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            LoadData();
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
            if (display) display.DisplayInfoWeapon1(ammoCarried, ammoInMagazine);

        }

        public void DisplayInfoWeapon2(int ammoCarried, int ammoInMagazine)
        {
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

        public override ObjectData CreateData()
        {
            PlayerData data = new PlayerData();

            //Living...
            data.RegisterEntityData(health, maxHealth, armorPoint, maxArmorPoint);

            data.marines = marinesType;
            data.ammo1 = playerAmmo.ammoCarriedType0;
            data.ammo2 = playerAmmo.ammoCarriedType1;
            
            if (marinesType == MarinesType.MajCarter)
            {
                data.weaponIndex = weapCCarter.IndexActiveWeapon;
                data.stockAmmo1 = weapCCarter.doubleGuns.magazine;
                data.stockAmmo2 = weapCCarter.flamethrower.magazine;
            }
            else
            {
                data.stockAmmo1 = weapCHartman.assaultRifle.magazineBullet;
                data.stockAmmo2 = weapCHartman.assaultRifle.magazineGrenade;
            }

            data.score = score;

            return data;
        }

        public override void LoadData()
        {
            PlayerData data = SaveSystem.GetPlayerData();

            if (data == null)
            {
                return;
            }

            health = data.health;
            maxHealth = data.maxHealth;
            armorPoint = data.armorPoint;
            maxArmorPoint = data.maxArmorPoint;

            playerAmmo.ammoCarriedType0 = data.ammo1;
            playerAmmo.ammoCarriedType1 = data.ammo2;

            if (marinesType == MarinesType.MajCarter)
            {

                if (weapCCarter.IndexActiveWeapon != data.weaponIndex)
                    weapCCarter.SwitchIndexWeapon();

                weapCCarter.doubleGuns.magazine = data.stockAmmo1;
                weapCCarter.flamethrower.magazine = data.stockAmmo2;
            }
            else
            {
                weapCHartman.assaultRifle.magazineBullet = data.stockAmmo1;
                weapCHartman.assaultRifle.magazineGrenade = data.stockAmmo2;
            }

            score = data.score;

        }

        public override void SaveObject()
        {
            BuildData();
            SaveSystem.SavePlayer(objectData as PlayerData);
        }
    }    
}

public enum MarinesType
{
    MajCarter, SgtHartman
}

[System.Serializable]
public class PlayerData : LivingEntityData
{
    public MarinesType marines;
    public int weaponIndex;
    public int ammo1;
    public int ammo2;
    public int stockAmmo1;
    public int stockAmmo2;
    public PlayerController.PlayerScore score;

    public PlayerData() : base(null) {}
}