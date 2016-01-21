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

        List<int> targetsAlreadyTouched;
        bool takedDmg;

        public float timerSet = 1.3f;
        float timer;
        
        // Use this for initialization
        void Start()
        {
            timer = Time.time + timerSet;
            // Debug.Log("Time.time : " + Time.time);
            // Debug.Log("timer : " + timer);

            targetsAlreadyTouched = new List<int>();
            explosion = gameObject.GetComponent<ParticleSystem>();
            Destroy(gameObject, explosion.duration);
        }

        void OnTriggerEnter(Collider other)
        {
            // Debug.Log(Time.time + " -- " + timer);
            if (Time.time < timer)
            {
                // Debug.Log("YOLO : " + other.gameObject.name);
                if (!targetsAlreadyTouched.Contains(other.gameObject.GetInstanceID()) && other.GetComponent<LivingEntity>() != null)
                {
                    targetsAlreadyTouched.Add(other.gameObject.GetInstanceID());
                    other.GetComponent<LivingEntity>().TakeDirectDamage(DoDamage());
                }
            }
        }

        int DoDamage()
        {
            return explosionDmg;
        }
    }
}
