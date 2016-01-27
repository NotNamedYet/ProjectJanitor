using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class GrenadeController : MonoBehaviour
    {

        public GameObject explosion;
        public float speed = 10f;
        Rigidbody body;

        [Tooltip("Set time to destroy the gameobject if no collide")]
        public float destroyTime = 2.5f;

        public int grenadeDmg;

        public GameObject damageSource;

        // Use this for initialization
        void Start()
        {
            body = gameObject.GetComponent<Rigidbody>();
            Destroy(gameObject, destroyTime);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            body.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
        }

        public void SetSource(GameObject source)
        {
            damageSource = source;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Wall" || other.tag == "Alien" || other.tag == "Indestructible Box") // Not finish, see if other tags must be add
            {
                Debug.Log("Im a grenade and i touch : " + other.tag.ToString() + ", i must be dead now");
                GameObject explo = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
                GrenadeExplosion gexplo = explo.GetComponent<GrenadeExplosion>();
                gexplo.explosionDmg = grenadeDmg;
                gexplo.damageSource = damageSource;

                Destroy(gameObject);
            }

        }
    } 
}
