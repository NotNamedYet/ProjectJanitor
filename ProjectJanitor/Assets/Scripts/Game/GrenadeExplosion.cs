using UnityEngine;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(ParticleSystem))]
    [RequireComponent(typeof(SphereCollider))]
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

        SphereCollider sphereCollider;
        public float blastSpreadingSpeed;
        public float maxBlastRadius;
        
        // Use this for initialization
        void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
            timer = Time.time + timerSet;

            GetComponent<AudioSource>().PlayOneShot(sndExplo);

            targetsAlreadyTouched = new List<int>();
            explosion = gameObject.GetComponent<ParticleSystem>();

            Destroy(gameObject, explosion.duration);
        }

        void Update()
        {
            if (sphereCollider.radius < maxBlastRadius)
                sphereCollider.radius += (Time.deltaTime * blastSpreadingSpeed);
        }

        public void SetSource(GameObject source)
        {
            damageSource = source;
        }

        void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
                return;

            if (Time.time < timer)
            {
                if (!targetsAlreadyTouched.Contains(other.gameObject.GetInstanceID()))
                {
                    IDamageable damageable = other.GetComponent(typeof(IDamageable)) as IDamageable;

                    if (damageable == null)
                    {
                        return;
                    }
                    targetsAlreadyTouched.Add(other.gameObject.GetInstanceID());
                    damageable.TakeDirectDamage(DoDamage());

                    //Target spreading.
                    if (damageable is AlienBase)
                    {
                        AlienBase alien = damageable as AlienBase;
                        alien.SetTarget(damageSource.transform);
                    }

                    if (damageable is CocoonSpawner)
                        (damageable as CocoonSpawner).TriggerSpawning(damageSource.transform);
                }
            }
        }

        int DoDamage()
        {
            return explosionDmg;
        }
    }
}
