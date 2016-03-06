using UnityEngine;
using System.Collections;

namespace MonoPersistency
{
    public abstract class MonoPersistent : MonoBehaviour
    {
        [Header("MonoPersistent")]

        [Tooltip("leave empty to use default MonoPersistent name")]
        public string specificName;
        [Tooltip("Will not be saved at runtime")]
        public bool m_volatile;
        public bool bypassBaking;

        protected DataContainer m_data;

        /// <summary>
        /// Use the parametter container to collect needed persistent data
        /// </summary>
        /// <param name="container"></param>
        public abstract void CollectData(DataContainer container);

        /// <summary>
        /// Use the container to restore data collected
        /// </summary>
        /// <param name="container"></param>
        public abstract void LoadData(DataContainer container);

        protected void LoadData()
        {
            LoadData(m_data);
        }

        /// <summary>
        /// Save this object in the Resgistery
        /// </summary>
        public virtual void Save()
        {
            if (!m_volatile)
            {
                if (m_data == null)
                    m_data = new DataContainer(gameObject.name);

                CollectData(m_data);

                SaveSystem.RegisterData(m_data);
            }
        }

        /// <summary>
        /// Register event OnUpdateRegisteryEvent.
        /// please call base.OnEnable() at the begining if you override this in hierachy.
        /// </summary>
        protected virtual void OnEnable()
        {
            SaveSystem.OnUpdateRegisteryEvent += Save;
        }

        /// <summary>
        /// UnRegister event OnUpdateRegisteryEvent.
        /// please call base.OnDisable() if you override this in hierachy.
        /// </summary>
        protected virtual void OnDisable()
        {
            SaveSystem.OnUpdateRegisteryEvent -= Save;
        }

        public void DisablePersistency()
        {
            m_volatile = true;
        }
    } 
}
