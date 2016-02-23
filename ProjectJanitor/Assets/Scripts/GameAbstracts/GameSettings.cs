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
            Debug.Log("GameSettings is instantiate in GameController");
            LoadPrefs();
        }

        #region Save
        /// <summary>
        /// Use it if multiple prefs exist and must be save
        /// </summary> 
        public void SavePrefs()
        {
            SaveSoundPrefs();
            Debug.Log("Prefs was saved in GameSettings");
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
            Debug.Log("Sound prefs loaded, master vol is at : " + _masterVol);
        }
        #endregion
    }
}
