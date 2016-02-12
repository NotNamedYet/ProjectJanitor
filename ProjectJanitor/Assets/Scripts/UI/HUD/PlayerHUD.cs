using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

namespace GalacticJanitor.UI
{
    public class PlayerHUD : MonoBehaviour
    {

        public EntityResourceDisplay display;

        // Use this for initialization
        void Start()
        {
            if (display)
            {
                GameController.Player.optionalDisplay = display;
                GameController.Player.UpdateDisplay();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
