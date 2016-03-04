using UnityEngine;
using System.Collections;

using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{

    public class SceneAmbiance : MonoBehaviour
    {

        [Tooltip("List of musics that can be played in the scene.")]
        public AudioClip[] musics;
        [Tooltip("List of sounds that can be played in the scene.")]
        public AudioClip[] ambiance;
        [Tooltip("Do you want play this sound randomly in the scene ? Same index's order to \"ambiance\" list.")]
        public bool[] soundsThatMustBePlayedRandomly;

        public AudioSource listenerMusic;
        public AudioSource listenerAmbiance;

        [Header("Scene Music Settings")]
        public bool playMusicAtStart;
        public int indexMusicAtStart;

        private int indexMusicPlayed;
        private bool _playingMusic;
        public bool PlayingMusic
        {
            get { return _playingMusic; }
            set { _playingMusic = value; }
        }

        [Header("Scene Ambiance Settings")]
        public bool playSoundsRandomlyAtStart;
        [Tooltip("Time minimum to wait before play a ambiance's sound after the last one")]
        public float rangeMin;
        [Tooltip("Time maximum to wait before play a ambiance's sound after the last one")]
        public float rangeMax;

        private bool _playingRandomlySoundsAmbiance;
        public bool PlayingRandomlySoundsAmbiance
        {
            get { return _playingRandomlySoundsAmbiance; }
            set { _playingRandomlySoundsAmbiance = value; }
        }
        

        // Use this for initialization
        void Awake()
        {
            GameController.SceneSounds = this;
            indexMusicPlayed = -1;
            if (ambiance.Length != soundsThatMustBePlayedRandomly.Length) // Check if unity user properly filled the fields
            {
                Debug.Log("Problem in SceneAmbianceManager : Do you filled up all the fields in the inspector ? See tooltips please. Asshole");
                _playingRandomlySoundsAmbiance = false; // Set to false to avoid BAMYA (Bad Allocation Memory in Your Assssss)
            }
        }

        void Start()
        {
            if (GameController.TopDownCamera != null)
            {
                transform.SetParent(GameController.TopDownCamera.transform);
                transform.localPosition = Vector3.zero;
            }

            if (playMusicAtStart)
            {
                PlayMusic(indexMusicAtStart);
            }

            if (playSoundsRandomlyAtStart)
            {
                _playingRandomlySoundsAmbiance = true;
                Invoke("PlaySoundsRdly", Random.Range(rangeMin, rangeMax));
            }
        }

        #region Music
        public void PlayMusic(int index)
        {
            index = Mathf.Abs(index);

            if (musics.Length > index)
            {
                if (index == indexMusicPlayed)
                {
                    if (listenerMusic.isPlaying) Debug.Log("Music already playing");
                    else StartCoroutine(CoroutPlayMusic());
                }
                else
                {
                    listenerMusic.Stop();
                    listenerMusic.clip = musics[index];
                    StartCoroutine(CoroutPlayMusic());
                }
            }
        }

        public void StopMusic()
        {
            StopCoroutine(CoroutPlayMusic());
            listenerMusic.Stop();
            _playingMusic = false;
            indexMusicPlayed = -1;
        }

        IEnumerator CoroutPlayMusic()
        {
            _playingMusic = true;
            float timeMusic = listenerMusic.clip.length;

            while (_playingMusic)
            {
                listenerMusic.Play();
                yield return new WaitForSeconds(timeMusic + 1f);
            }
        }
        #endregion

        #region Ambiance
        public void PlayAmbianceSound(AudioSource source, int index)
        {
            index = Mathf.Abs(index);

            if (ambiance.Length > index)
            {
                ResetSettingsOnAudioSrcAmbiance();
                source.PlayOneShot(ambiance[index]);
            }
        }

        public void DoRandomSettingsOnAudioSrcAmbiance()
        {
            listenerAmbiance.panStereo = Random.Range(-1.0f, 1.0f);
        }

        public void ResetSettingsOnAudioSrcAmbiance()
        {
            listenerAmbiance.panStereo = 0; 
        }
        #endregion

        #region SceneSettings
        public void PlaySoundsRdly()
        {
            if (ambiance.Length == soundsThatMustBePlayedRandomly.Length) // again, protection against BAMYA
            {
                StartCoroutine(CoroutPlayAmbianceSounds());
            }
        }

        IEnumerator CoroutPlayAmbianceSounds()
        {
            while(_playingRandomlySoundsAmbiance)
            {
                int soundToPlay = ChooseSoundToPlay();
                DoRandomSettingsOnAudioSrcAmbiance();
                listenerAmbiance.PlayOneShot(ambiance[soundToPlay]);
                yield return new WaitForSeconds(ambiance[soundToPlay].length + (Random.Range(rangeMin, rangeMax)));
            }
            ResetSettingsOnAudioSrcAmbiance();
        }

        /// <summary>
        /// Return an AudioClip randomly generated, using "soundsThatMustBePlayedRandomly" and "ambiance".
        /// </summary>
        /// <returns></returns>
        private int ChooseSoundToPlay()
        {
            ArrayList list = new ArrayList();

            for (int x = 0; x < soundsThatMustBePlayedRandomly.Length; x++)
            {
                if (soundsThatMustBePlayedRandomly[x] == true) list.Add(x); // If true, ref the AudioSource in the temporary list
            }

            return (int)list[Random.Range(0, list.Count)];
        }
        #endregion

    }

}
