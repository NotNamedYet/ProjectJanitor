using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GalacticJanitor.Engine;
using MonoPersistency;
using System.Collections.Generic;

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

        public PanelSaveHeader header;
        
        public RegisterySnapshot selectedSnapshot;

        //[HideInInspector]
        public bool newSave;

        List<Button> population;

        void Awake()
        {
            population = new List<Button>();
        }

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
        }

        void OnEnable()
        {
            Refresh();
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
            Refresh();
        }

        void SaveGame()
        {
            if (newSave)
            {
                Debug.Log("NewSave");
                SaveSystem.SaveAndWrite(true);
            }
            else
            {
                if (selectedSnapshot != null)
                {
                    Debug.Log("OverrideSave");
                    SaveSystem.SaveAndWrite(selectedSnapshot);
                }
            }
            Refresh();
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
                psb.m_PanelHolder = this;
                psb.newSave = true;
                psb.m_LabelText.text = "New...";
                psb.m_TimePlayedText.text = "";
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
                psb.m_PanelHolder = this;
                psb.m_LabelText.text = snap.PartyName;
                psb.m_TimePlayedText.text = snap.FormatTimePlayed;
                psb.m_ContextSnap = snap;

                if (panelContext == PanelContext.SAVE)
                    psb.newSave = false;
            }
        }
        

        Button InstanciateButton()
        {
            Button button = Instantiate(contentButton);
            button.transform.SetParent(dynamicContentHolder.transform);
            button.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            population.Add(button);

            return button;
        }

        public void Refresh()
        {
            selectedSnapshot = null;

            if (header)
            {
                header.DisplayCustomInfo((saveContext())? "Save your game" : "Load a Party", null, null);
            }

            if (population.Count > 0)
            {
                foreach(Button btn in population)
                {
                    Destroy(btn.gameObject);
                }
                population.Clear();
            }
            AllowValidation(false);
            PopupateList();
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