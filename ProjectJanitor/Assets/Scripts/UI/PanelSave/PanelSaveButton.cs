using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MonoPersistency;

namespace GalacticJanitor.UI
{
    public class PanelSaveButton : MonoBehaviour
    {
        
        public Text m_LabelText;
        public Text m_TimePlayedText;
        public RegisterySnapshot m_ContextSnap;
        public PanelSaveSystem m_PanelHolder;
        public bool newSave = true;


        public void SetSelected()
        {

            if (m_PanelHolder.panelContext == PanelSaveSystem.PanelContext.LOAD)
            {
                m_PanelHolder.selectedSnapshot = m_ContextSnap;
                m_PanelHolder.AllowValidation(true);

                if (m_PanelHolder.header)
                    m_PanelHolder.header.FillHeaderInfo(m_ContextSnap);
            }
            else
            {
                if (!newSave)
                {
                    m_PanelHolder.selectedSnapshot = m_ContextSnap;
                    if (m_PanelHolder.header)
                        m_PanelHolder.header.FillHeaderInfo(m_ContextSnap);
                }
                else
                {
                    if (m_PanelHolder.header)
                        m_PanelHolder.header.DisplayCustomInfo("Create new Save", null, null);
                }

                m_PanelHolder.newSave = newSave;
                m_PanelHolder.AllowValidation(true);
            }
            
        }
    } 
}
