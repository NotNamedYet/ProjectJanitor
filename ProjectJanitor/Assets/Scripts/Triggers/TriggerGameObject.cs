using UnityEngine;
using System.Collections;
using MonoPersistency;
using System;

namespace GalacticJanitor.Game
{
    public class TriggerGameObject : MonoBehaviour
    {

        [Header("TriggerGameObject settings")]
        [Tooltip("Game object(s) to activate")]
        public GameObject[] obj;

        protected bool m_Active = true;

        // Use this for initialization
        void Start()
        {
            if (m_Active)
            {
                foreach(GameObject tar in obj)
                {
                    tar.SetActive(false);
                }
            }
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
