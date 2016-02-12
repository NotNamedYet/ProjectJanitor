using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GalacticJanitor.Engine;
using MonoPersistency;

namespace GalacticJanitor.UI
{
    public class PanelSaveSystem : MonoBehaviour
    {

        public PanelContext panelContext;
        public GameObject dynamicContentHolder;
        public Button contentButton;
        public Button validationButton;
        //public Text headerLabel;
        public Text validationButtonLabel;

        [HideInInspector]
        public RegisterySnapshot selectedSnapshot;

        //[HideInInspector]
        public bool newSave;

        

        void Start()
        {
            if (saveContext())
            {
                //headerLabel.text = "save game";
                validationButtonLabel.text = "save";
            }
            else if (loadContext())
            {
                //headerLabel.text = "load game";
                validationButtonLabel.text = "load";
            }

            PopupateList();
            AllowValidation(false);
        }

        // ACTION 

        public void ExectuteAction()
        {
            if (panelContext == PanelContext.LOAD)
            {
                LoadGame();
            }
            else if (panelContext == PanelContext.SAVE)
            {
                SaveGame();
            }
        }

        void LoadGame()
        {
            if (selectedSnapshot != null)
            {
                GameController.ExitPause();
                SaveSystem.LoadGame(selectedSnapshot);
            }
            else
            {
                Debug.Log("Nope..");
            }
        }

        void SaveGame()
        {
            if (newSave)
            {
                SaveSystem.SaveAndWrite(true);
            }
            else
            {
                if (selectedSnapshot != null)
                {
                    SaveSystem.SaveAndWrite(false);
                }
            }
        }

        public void AllowValidation(bool value)
        {
            validationButton.interactable = value;
        }


        //LIST STRUCTURE
        
        void PopupateList()
        {
            if (panelContext == PanelContext.SAVE)
            {
                Button button = InstanciateButton();
                PanelSaveButton psb = button.GetComponent<PanelSaveButton>();
                psb.panel = this;
                psb.newSave = true;
                psb.label.text = "new Save";
            }
            GenerateSnapshotButtons();
        }

        
        void GenerateSnapshotButtons()
        {
            RegisterySnapshot[] snaps = SaveSystem.LoadSnapshots();

            foreach (var snap in snaps)
            {
                Button button = InstanciateButton();

                PanelSaveButton psb = button.GetComponent<PanelSaveButton>();
                psb.panel = this;
                psb.label.text = snap.m_identifier;
                psb.linkedSnap = snap;

                if (panelContext == PanelContext.SAVE)
                    psb.newSave = false;
            }
        }
        

        Button InstanciateButton()
        {
            Button button = Instantiate(contentButton);
            button.transform.SetParent(dynamicContentHolder.transform);
            button.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            return button;
        }

        

        public enum PanelContext {
            SAVE,
            LOAD
        }

        bool saveContext()
        {
            return panelContext == PanelContext.SAVE;
        }

        bool loadContext()
        {
            return panelContext == PanelContext.LOAD;
        }
    } 
}