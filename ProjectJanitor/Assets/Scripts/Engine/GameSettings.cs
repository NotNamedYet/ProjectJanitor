using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{

    /// <summary>
    /// Handle PlayerPrefs
    /// </summary>
    public class GameSettings
    {

        private float _masterVol;

        public float MasterVol
        {
            get { return _masterVol; }
            set
            {
                _masterVol = value;
                SaveSoundPrefs();
            }
        }

        public GameSettings()
        {
            Debug.Log("[System] GameSettings --Loading");
            LoadPrefs();
        }

        #region Save
        /// <summary>
        /// Use it if multiple prefs exist and must be save
        /// </summary> 
        public void SavePrefs()
        {
            Debug.Log("[System] GameSettings --Saving");
            SaveSoundPrefs();
        }

        public void SaveSoundPrefs()
        {
            PlayerPrefs.SetFloat("masterVol", _masterVol);
        }
        #endregion

        #region Load
        public void LoadPrefs()
        {
            LoadSoundPrefs();
        }

        public void LoadSoundPrefs()
        {
            _masterVol = PlayerPrefs.GetFloat("masterVol");
            Debug.Log("[System] GameSettings : MasterVolume: " + _masterVol);
        }
        #endregion
    }
}
