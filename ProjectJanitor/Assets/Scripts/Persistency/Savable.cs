using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Persistency
{
    public class Savable : MonoBehaviour
    {

        [UniqueIdentifier]
        public string uniqueID;

        public SavableObject savableObject;

        // Use this for initialization
        void Start()
        {
            SpawnObject();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (savableObject != null) savableObject.Save();
            }
        }

        void SpawnObject()
        {
            if (GameController.Controller != null)
            {
                bool canSpan = true;
                ObjectData data = null;

                if (GameController.Controller.Registery.objectData.ContainsKey(uniqueID))
                {
                    data = GameController.Controller.Registery.objectData[uniqueID];
                    canSpan = data.canSpawn;
                }
                
                if (canSpan)
                {
                    savableObject = Instantiate(savableObject, transform.position, transform.rotation) as SavableObject;
                    savableObject.uniqueId = uniqueID;
                    savableObject.transform.SetParent(gameObject.transform);
                    if (data != null)
                    {
                        savableObject.SetData(data);
                    }
                       
                }

            }
        }
    } 
}
