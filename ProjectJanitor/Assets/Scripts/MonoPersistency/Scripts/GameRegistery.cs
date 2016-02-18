using UnityEngine;
using System.Collections.Generic;
using GalacticJanitor.Game;

namespace MonoPersistency
{
    [System.Serializable]
    public class GameRegistery
    {
        public RegisterySnapshot m_snapshot;
        public bool m_firstRegistering = true;
        public DataContainer m_player;
        public Dictionary<int, SceneData> m_scenes { get; private set; }

        public GameRegistery(string startlevel)
        {
            m_snapshot = new RegisterySnapshot();
            m_snapshot.m_currentScene = startlevel;
            NewID();
            m_scenes = new Dictionary<int, SceneData>();
        }

        /// <summary>
        /// Attribute a New identifier (A new file will be created on next save)
        /// </summary>
        public void NewID()
        {
            m_snapshot.m_identifier = "GameSave" + System.DateTime.Now.ToBinary();
        }
    } 

    [System.Serializable]
    public class RegisterySnapshot
    {
        public string m_identifier;
        public string m_currentScene;
        public long m_timePlayed;
    }

    [System.Serializable]
    public class SceneData
    {
        public int m_sceneIndex;
        public StageData m_stage;
        public Dictionary<string, DataContainer> m_datas { get; private set; }

        public SceneData(int buildIndex)
        {
            m_sceneIndex = buildIndex;
            m_datas = new Dictionary<string, DataContainer>();
        }
    }

    [System.Serializable]
    public class StageData
    {
        public bool m_disallowPlayer;
        public SerializedVector3 m_playerPos;
        public SerializedQuaternion m_playerRot;

        public StageData(Vector3 playerPos, Quaternion playerRot, bool disallowPlayer)
        {
            m_playerPos = Serializer.Serializevector3(playerPos);
            m_playerRot = Serializer.SerializeQuaternion(playerRot);
            this.m_disallowPlayer = disallowPlayer;
        }

        /// <summary>
        /// Output rotation and the Position of the player stored into this StageData
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        public void TranslateLocation(out Vector3 pos, out Quaternion rot)
        {
            pos = Serializer.DeserializeVector3(m_playerPos);
            rot = Serializer.DeserializeQuaternion(m_playerRot);
        }

        /// <summary>
        /// Restore the given transform position/rotation of the player for this stage
        /// </summary>
        /// <param name="transfrom"></param>
        public void RestorePlayerLocation(Transform transfrom)
        {
            transfrom.position = Serializer.DeserializeVector3(m_playerPos);
            transfrom.rotation = Serializer.DeserializeQuaternion(m_playerRot);
        }

        /// <summary>
        /// Translate to a serializable form the position and rotation of the given transfrom
        /// to register the last player location of this stage
        /// </summary>
        /// <param name="transform"></param>
        public void RegisterPlayerLocation(Transform transform)
        {
            m_playerPos = Serializer.Serializevector3(transform.position);
            m_playerRot = Serializer.SerializeQuaternion(transform.rotation);
        }
    }
}
