using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;
using System.Collections.Generic;

namespace GalacticJanitor.Persistency
{
    public abstract class SavableObject : MonoBehaviour
    {

        [HideInInspector] public string uniqueId;

        protected abstract ObjectData BuildData();
        protected abstract void RestoreObjectState();

        protected ObjectData LoadDataFromRegistery()
        {
            try
            {
                return GameController.Controller.Registery.objectData[uniqueId];
            }
            catch(KeyNotFoundException)
            {
                return null;
            }
        }

        public void Save()
        {
            Registery reg = GameController.Controller.Registery;

            if (reg.objectData.ContainsKey(uniqueId))
            {
                reg.objectData[uniqueId] = BuildData();
            }
            else
            {
                reg.objectData.Add(uniqueId, BuildData());
            }

        }

        void OnDestroy()
        {
            Save();
        }

    }

    [System.Serializable]
    public abstract class ObjectData
    {
        public string uniqueId;
        public bool canSpawn = true;

        public ObjectData(string uniqueId)
        {
            this.uniqueId = uniqueId;
        }


    } 
}
