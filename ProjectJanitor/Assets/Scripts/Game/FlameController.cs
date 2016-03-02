using UnityEngine;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(Collider))]
    public class FlameController : MonoBehaviour
    {

        [HideInInspector]
        public int flameDmg;

        private bool canDamageThisFrame;
        public float timer;
        private float timerActive; // Timer in game

        public List<int> targetsBurning;

        [Header("Sounds", order = 1)]
        public AudioClip sndBegin;
        public AudioClip sndBurning;

        private AudioSource listener;

        // Use this for initialization
        void Start()
        {
            listener = GetComponent<AudioSource>();
            targetsBurning = new List<int>();
            listener.PlayOneShot(sndBegin);
        }

        // Update is called once per frame
        void Update()
        {
            FlameCanDamageTimer();
            PlaySounds();
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
            if (!targetsBurning.Contains(other.gameObject.GetInstanceID()) && other.GetComponent<LivingEntity>() && other.tag != "Player")
            {
                targetsBurning.Add(other.gameObject.GetInstanceID());
                other.GetComponent<LivingEntity>().TakeDirectDamage(DoDamage());
                Debug.Log(other.tag);
            }
        }

        int DoDamage()
        {
            return flameDmg;
        }

        private void PlaySounds()
        {
            if (!listener.isPlaying) listener.PlayOneShot(sndBurning);
        }

    }

}