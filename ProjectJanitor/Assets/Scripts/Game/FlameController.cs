using UnityEngine;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(Collider))]
    public class FlameController : MonoBehaviour
    {

        [HideInInspector]
        public int flameDmg;
        public GameObject damageSource;
        private bool canDamageThisFrame;
        public float timer;
        private float timerActive; // Timer in game

        public List<int> targetsBurning;

        [Header("Sounds", order = 1)]
        //public AudioClip sndBegin; Deprecated
        //public AudioClip sndBurning; Deprecated
        public AudioClip sndFlame;

        private AudioSource listener;

        // Use this for initialization
        void Start()
        {
            listener = GetComponent<AudioSource>();
            targetsBurning = new List<int>();
            if (sndFlame) listener.PlayOneShot(sndFlame);
        }

        // Update is called once per frame
        void Update()
        {
            FlameCanDamageTimer();
            //PlaySounds();
        }

        private void FlameCanDamageTimer()
        {
            timerActive += Time.deltaTime;
            if (timerActive >= timer)
            {
                timerActive = 0f;
                targetsBurning.Clear();
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
                return;

            if (!targetsBurning.Contains(other.gameObject.GetInstanceID()))
            {
                targetsBurning.Add(other.gameObject.GetInstanceID());

                IDamageable damageable = other.GetComponent(typeof(IDamageable)) as IDamageable;

                if (damageable != null)
                {
                    damageable.TakeDirectDamage(DoDamage());

                    if (damageable is AlienBase)
                        (damageable as AlienBase).SetTarget(damageSource.transform);

                    if (damageable is CocoonSpawner)
                        (damageable as CocoonSpawner).TriggerSpawning(damageSource.transform);
                }
            }
        }

        int DoDamage()
        {
            return flameDmg;
        }

        /*private void PlaySounds()
        {
            if (!listener.isPlaying) listener.PlayOneShot(sndBurning);
        }*/

    }

}