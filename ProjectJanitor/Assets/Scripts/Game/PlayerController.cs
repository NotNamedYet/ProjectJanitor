using UnityEngine;
using System.Collections;

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

        Rigidbody body;
        public PlayerRotation rotate; // Ref to the gameObject that must rotate, with the script PlayerRotation

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
            livingEntity = gameObject.GetComponent<LivingEntity>();
            playerAmmo = gameObject.GetComponent<PlayerAmmo>();
            body = gameObject.GetComponent<Rigidbody>();
        }

        void Update()
        {
            rotate.ForceLookAt();
            UpdateCooldown();
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
    }

    public enum MarinesType
    {
        MajCarter, SgtHartman
    }
}