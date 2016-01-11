using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GalacticJanitor.Persistency;
using GalacticJanitor.Engine;

namespace GalacticJanitor.UI
{
    public class PanelSaveSystem : MonoBehaviour
    {

        public PanelContext panelContext;
        public GameObject dynamicContentHolder;
        public Button contentButton;
        public Button validationButton;
        public Text headerLabel;
        public Text validationButtonLabel;

        [HideInInspector]
        public RegisterySnapshot selectedSnapshot;

        //[HideInInspector]
        public bool newSave;

        

        void Start()
        {
            if (saveContext())
            {
                headerLabel.text = "Save Game";
                validationButtonLabel.text = "Save";
            }
            else if (loadContext())
            {
                headerLabel.text = "Load Game";
                validationButtonLabel.text = "Load";
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
                GameController.Controller.LoadGameFromSnapshot(selectedSnapshot);
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
                GameController.Controller.SaveRegistery(true);
            }
            else
            {
                if (selectedSnapshot != null)
                {
                    GameController.Controller.SaveRegistery(selectedSnapshot);
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
                psb.label.text = "New Save";
            }
            GenerateSnapshotButtons();
        }

        void GenerateSnapshotButtons()
        {
            RegisterySnapshot[] snaps = GameController.Controller.LoadAllSnapshots();

            foreach (var snap in snaps)
            {
                Button button = InstanciateButton();

                PanelSaveButton psb = button.GetComponent<PanelSaveButton>();
                psb.panel = this;
                psb.label.text = snap.identifier;
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
