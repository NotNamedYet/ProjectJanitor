using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class GrenadeMove : MonoBehaviour
    {

        public GameObject explosion;
        public float speed = 10f;
        Rigidbody body;

        [Tooltip("Set time to destroy the gameobject if no collide")]
        public float destroyTime = 2.5f;

        // Use this for initialization
        void Start()
        {
            body = gameObject.GetComponent<Rigidbody>();
            Destroy(gameObject, destroyTime);
        }

        // Update is called once per frame
        void Update()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            body.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
            // transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Wall" || other.tag == "Alien" || other.tag == "Indestructible Box") // Not finish
            {
                Debug.Log("Im a grenade and i touch : " + other.tag.ToString() + ", i must be dead now");
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }
    } 
}
