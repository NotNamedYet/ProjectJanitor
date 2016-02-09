using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{
    public class BulletController : MonoBehaviour
    {

        public float speed = 10f;
        Rigidbody body;

        [Tooltip("Set time to destroy the gameobject if no collide")]
        public float destroyTime = 2.5f;

        public int bulletDmg;
        public GameObject damageSource;

        void Awake()
        {
            transform.SetParent(GameController.ProjectileHolder);
            body = gameObject.GetComponent<Rigidbody>();
        }

        // Use this for initialization
        void Start()
        {
            
            Destroy(gameObject, destroyTime);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            MoveForward();
        }

        public void SetSource(GameObject source)
        {
            damageSource = source;
        }

        private void MoveForward()
        {
            body.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Wall" || other.tag == "Alien" || other.tag == "Indestructible Box") // Not finish, see if other tags must be add
            {
                Debug.Log("Im a bullet and i touch : " + other.tag.ToString() + ", i must be dead now");
                if (other.GetComponent<LivingEntity>() != null)
                {
                    LivingEntity target = other.GetComponent<LivingEntity>();
                    target.TakeDirectDamage(DoDamage());

                    if (other.tag == "Alien" && damageSource != null)
                    {
                        if (other.GetComponent<AlienBase>().target == null)
                        {
                            other.GetComponent<AlienBase>().target = damageSource.transform;
                        }
                    }
                }

                //Debug
                if (other.GetComponent<LivingEntity>() == null && other.tag == "Alien")
                {
                    Debug.Log("We have a problem with an invincible alien in BulletController, wtf");
                }
                Destroy(gameObject);
            }  
        }

        int DoDamage()
        {
            return bulletDmg;
        }
    }
}