using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{

    public class MixSounds : MonoBehaviour
    {

        public AudioMixer master;

        // Use this for initialization
        void Start()
        {
            GameController.Controller.settings.MasterVol = 0f; // In construction
            SetMasterVol(GameController.Controller.settings.MasterVol);
        }

        public void SetMasterVol(float vol)
        {
            master.SetFloat("masterVol", vol);
        }
    }
     
}
