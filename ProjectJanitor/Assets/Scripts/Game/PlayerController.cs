using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {

        public float speed = 10;
        public Rigidbody body;
        public PlayerRotation rotate; // Ref to the gameObject that must rotate, with the script PlayerRotation
        // Use this for initialization
        void Start()
        {
            body = gameObject.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Movement();
        }

        void Movement()
        {
            rotate.ForceLookAt();
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            body.AddForce(move * speed, ForceMode.VelocityChange); // For Forcemode see 2
        }
    }
}