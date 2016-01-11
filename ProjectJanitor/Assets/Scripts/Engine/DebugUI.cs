using UnityEngine;
using System.Collections;
using GalacticJanitor.Persistency;

namespace GalacticJanitor.Engine
{
    public class DebugUI : MonoBehaviour
    {

        private static DebugUI debug;

        void Awake()
        {
            if (debug == null)
            {
                DontDestroyOnLoad(gameObject);
                debug = this;
            }
            else if (debug != this)
            {
                Destroy(gameObject);
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 150, 20), "Yolo"))
            {
                Savable[] s = FindObjectsOfType<Savable>();
                foreach (Savable one in s)
                {
                    Debug.Log(one.uniqueID);
                    //one.savable.Save();
                }
            }

            Registery r = GameController.Controller.Registery;

            if (GUI.Button(new Rect(10, 30, 150, 20), "Yolo2"))
            {
                foreach (string id in r.objectData.Keys)
                {

                    Debug.Log(r.objectData[id].ToString());
                }

            }
        }
    } 
}
