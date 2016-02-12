using UnityEngine;
using System.Collections;

namespace MonoPersistency
{
    [RequireComponent(typeof(MonoPersistent))]
    public class MonoPersistentInitializer : MonoBehaviour
    {
        public bool isPlayer;

        void Start()
        {
            Init();
        }

        /// <summary>
        /// Notify the SaveSystem that the MonoPersistent object need to be loaded.
        /// </summary>
        public void Init()
        {
            if (!isPlayer) SaveSystem.LoadMonoPersistent(gameObject.name, GetComponent<MonoPersistent>());
        }
        
    } 
}
