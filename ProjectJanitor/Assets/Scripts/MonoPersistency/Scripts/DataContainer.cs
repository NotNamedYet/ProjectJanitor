using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MonoPersistency
{
    [System.Serializable]
    public class DataContainer
    {
        public readonly string m_key;
        public bool m_spawnable = true;

        Hashtable m_table;
        Dictionary<string, SerializedVector3> m_vectors;
        Dictionary<string, SerializedQuaternion> m_quaternions;

        SerializedVector3 m_position;
        SerializedVector3 m_scale;
        SerializedQuaternion m_rotation;
        bool m_scaleRegistered;
        EntityTemplate m_entity;

        /// <summary>
        /// The key is used to identify this instance into the GameRegistery
        /// </summary>
        /// <param name="key">The unique key of this DataContainer</param>
        public DataContainer(string key)
        {
            m_key = key;
        }

        public void RegisterLocation(Transform transform, bool registerScale)
        {
            m_position = Serializer.Serializevector3(transform.position);
            m_rotation = Serializer.SerializeQuaternion(transform.rotation);

            if (registerScale)
            {
                m_scale = Serializer.Serializevector3(transform.localScale);
                m_scaleRegistered = true;
            }
        }

        public void RegisterLocation(Transform transform)
        {
            RegisterLocation(transform, false);
        }

        public void RestoreLocation(Transform transform)
        {
            transform.position = Serializer.DeserializeVector3(m_position);
            transform.rotation = Serializer.DeserializeQuaternion(m_rotation);

            if (m_scaleRegistered)
            {
                transform.localScale = Serializer.DeserializeVector3(m_scale);
            }
        }

        public void AddVector3(string key, Vector3 v3)
        {
            SerializedVector3 _sv3 = Serializer.Serializevector3(v3);

            if (m_vectors == null)
            {
                m_vectors = new Dictionary<string, SerializedVector3>();
                m_vectors.Add(key, _sv3);
            }
            else
            {
                if (m_vectors.ContainsKey(key))
                {
                    m_vectors[key] = _sv3;
                }
                else
                {
                    m_vectors.Add(key, _sv3);
                }
            }
        }

        public void AddQuaternion(string key, Quaternion quat)
        {
            SerializedQuaternion _sqtn = Serializer.SerializeQuaternion(quat);

            if (m_quaternions == null)
            {
                m_quaternions = new Dictionary<string, SerializedQuaternion>();
                m_quaternions.Add(key, _sqtn);
            }
            else
            {
                if (m_quaternions.ContainsKey(key))
                {
                    m_quaternions[key] = _sqtn;
                }
                else
                {
                    m_quaternions.Add(key, _sqtn);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">!important Need to be serializable</param>
        public void Addvalue(string key, object value)
        {
            if (m_table == null) m_table = new Hashtable();

            if (m_table.ContainsKey(key))
            {
                m_table[key] = value;
            }
            else
            {
                m_table.Add(key, value);
            }
        }

        public T GetValue<T>(string key)
        {
            return (T)m_table[key];
        }


        public void AddEntityTemplate(string name, int health, int maxHealth, int armor, int maxArmor)
        {
            m_entity = new EntityTemplate();

            m_entity.m_name = name;
            m_entity.m_health = health;
            m_entity.m_maxHealth = maxHealth;
            m_entity.m_armor = armor;
            m_entity.m_maxArmor = maxArmor;
        }

        public void RestoreEntityTemplate(out string name, out int health, out int maxHealth, out int armor, out int maxArmor)
        {
            name = m_entity.m_name;
            health = m_entity.m_health;
            maxHealth = m_entity.m_maxHealth;
            armor = m_entity.m_armor;
            maxArmor = m_entity.m_maxArmor;
        }

        [System.Serializable]
        struct EntityTemplate
        {
            public string m_name;
            public int m_health;
            public int m_maxHealth;
            public int m_armor;
            public int m_maxArmor;
        }
    } 
}
