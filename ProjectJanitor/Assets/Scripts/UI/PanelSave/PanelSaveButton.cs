using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MonoPersistency;

namespace GalacticJanitor.UI
{
    public class PanelSaveButton : MonoBehaviour
    {
        
        public Text label;
        public RegisterySnapshot linkedSnap;
        public PanelSaveSystem panel;
        public bool newSave = true;


        public void SetSelected()
        {

            if (panel.panelContext == PanelSaveSystem.PanelContext.LOAD)
            {
                panel.selectedSnapshot = linkedSnap;
                panel.AllowValidation(true);
            }
            else
            {
                if (!newSave)
                    panel.selectedSnapshot = linkedSnap;

                panel.newSave = newSave;
                panel.AllowValidation(true);
            }

        }
    } 
}
