using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{

    public class SoundSourceEnvironement : MonoBehaviour
    {

        public AudioClip[] sound;

        public AudioSource listener;

        [Tooltip("If you want destroy the object after trigger")]
        public bool destroyMeAfterTrigger;

        // Use this for initialization
        void Awake()
        {
            listener = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) // Debug
            {
                listener.clip = sound[0];
                listener.Play();
            }
        }

        #region PlaySounds

        /// <summary>
        /// Play a sound, form scene ambiance class or from somewhere else.
        /// Return false if already playing a sound. Yes, i know, that's life.
        /// </summary>
        public bool PlaySoundOneShot(AudioClip snd)
        {
            if (listener.isPlaying)
            {
                return false;
            }
            else
            {
                if (!listener.isPlaying) listener.PlayOneShot(snd);
                return true;
            }
        }

        /// <summary>
        /// Play sound LOGGED (important !!!) in the audio source.
        /// </summary>
        public void PlaySoundInAudioSource()
        {
            if (listener.clip == null)
            {
                listener.clip = sound[0];
            }

            if (!listener.isPlaying) listener.Play();
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                PlaySoundInAudioSource();

                if (destroyMeAfterTrigger)
                {
                    Destroy(gameObject, listener.clip.length + 0.1f);
                }

            }
        }

        #endregion

    }

}