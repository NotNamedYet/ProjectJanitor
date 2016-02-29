using UnityEngine;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(Collider))]
    public class GrenadeExplosion : MonoBehaviour
    {

        [HideInInspector]
        public int explosionDmg;
        ParticleSystem explosion;

        List<int> targetsAlreadyTouched; // Use to manage damage on target, the purpose is that target can only be damageable one time
        bool takedDmg;

        [Tooltip("How much time the explosion can do damage, related to the particule explosion")]
        public float timerSet = 0.75f;
        float timer;

        public AudioClip sndExplo;

        public GameObject damageSource;

        
        // Use this for initialization
        void Start()
        {
            timer = Time.time + timerSet;

            GetComponent<AudioSource>().PlayOneShot(sndExplo);

            targetsAlreadyTouched = new List<int>();
            explosion = gameObject.GetComponent<ParticleSystem>();
            Destroy(gameObject, explosion.duration);
        }

        public void SetSource(GameObject source)
        {
            damageSource = source;
        }

        void OnTriggerEnter(Collider other)
        {
            if (Time.time < timer)
            {
                if (!targetsAlreadyTouched.Contains(other.gameObject.GetInstanceID()) && other.GetComponent<LivingEntity>() != null)
                {
                    targetsAlreadyTouched.Add(other.gameObject.GetInstanceID());
                    other.GetComponent<LivingEntity>().TakeDirectDamage(DoDamage());

                    if (other.tag == "Alien" && damageSource != null)
                    {
                        if (other.GetComponent<AlienBase>().target == null)
                        {
                            other.GetComponent<AlienBase>().SetTarget(damageSource.transform);
                        }
                    }
                }
            }
        }

        int DoDamage()
        {
            return explosionDmg;
        }
    }
}
