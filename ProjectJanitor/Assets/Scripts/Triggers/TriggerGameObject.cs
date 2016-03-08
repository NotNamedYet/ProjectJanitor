using UnityEngine;
using System.Collections;
using MonoPersistency;
using System;

namespace GalacticJanitor.Game
{

    [ExecuteInEditMode]
    public class TriggerGameObject : MonoBehaviour
    {

        [Header("TriggerGameObject settings")]
        [Tooltip("Game object(s) to activate")]
        public GameObject[] obj;

        protected bool m_Active = true;


        void Update()
        {
#if UNITY_EDITOR
            if (obj != null)
                foreach (GameObject c in obj)
                {
                    if (c) Debug.DrawLine(transform.position, c.transform.position, Color.blue);
                }
#endif
        }

        // Use this for initialization
        void Start()
        {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
            if (m_Active)
            {
                foreach(GameObject tar in obj)
                {
                    tar.SetActive(false);
                }
            }
#endif
        }

        public void ActiveGO()
        {
            if (m_Active)
            {
                foreach(GameObject tar in obj)
                {
                    tar.SetActive(true);
                }

                m_Active = false;
            }
        }
    } 

}
