using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Persistency
{
    public abstract class SavableObject : MonoBehaviour
    {

        [HideInInspector] public string uniqueId;

        public abstract ObjectData GetData();
        public abstract void SetData(ObjectData data);

        public void Save()
        {
            Registery reg = GameController.Controller.Registery;

            if (reg.objectData.ContainsKey(uniqueId))
            {
                reg.objectData[uniqueId] = GetData();
            }
            else
            {
                reg.objectData.Add(uniqueId, GetData());
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
