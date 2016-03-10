using UnityEngine;
using System.Collections.Generic;
using GalacticJanitor.Game;
using System.Text;
using System;

namespace MonoPersistency
{
    [System.Serializable]
    public class GameRegistery
    {
        public RegisterySnapshot m_snapshot;
        public bool m_firstRegistering = true;
        public DataContainer m_player;
        public Dictionary<string, SceneData> m_scenes { get; private set; }

        public GameRegistery(string startlevel)
        {
            m_snapshot = new RegisterySnapshot();
            m_snapshot.m_lastUpdate = DateTime.Now.ToBinary();
            m_snapshot.m_currentScene = startlevel;
            NewID();
            m_scenes = new Dictionary<string, SceneData>();
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
    public class RegisterySnapshot : IComparable<RegisterySnapshot>
    {
        #region Properties
        public string FormatTimePlayed
        {
            get
            {
                return FormatTime();
            }
        }

        public string PartyName
        {
            get
            {
                if (string.IsNullOrEmpty(m_partyName))
                    m_partyName = "MissingPartyName";

                return m_partyName;
            }
            set { m_partyName = value;}
        }
        #endregion

        public string m_identifier;
        public string m_partyName;
        public string m_currentScene;
        public long m_timePlayed;
        public long m_lastUpdate;

        

        string FormatTime()
        {
            long secs = 0, mins = 0, hrs = 0, days = 0;

            secs = m_timePlayed % 60;
            mins = m_timePlayed / 60;

            if (mins > 60)
            {
                hrs = mins / 60;
                mins %= 60;

                if (hrs > 60)
                {
                    days = hrs / 24;
                    hrs %= 24;
                }
            }

            StringBuilder buffer = new StringBuilder();

            if (days > 0)
            {
                buffer.Append(days).Append('d');

                if (hrs > 0)
                    buffer.Append(' ').Append(hrs).Append('h');

            }
            else if (hrs > 0)
            {
                buffer.Append(hrs).Append('h');

                if (mins > 0)
                    buffer.Append(' ').Append(mins).Append('m');
            }
            else
            {
                buffer.Append(mins).Append("m");

                if (secs > 0)
                    buffer.Append(' ').Append(secs).Append('s');
            }

            return buffer.ToString();
        }

        public int CompareTo(RegisterySnapshot other)
        {
            return m_lastUpdate.CompareTo(other.m_lastUpdate);
        }
    }

    [System.Serializable]
    public class SceneData
    {
        StageData m_stageData;
        public StageData StageData {
            get
            {
                if (m_stageData == null)
                    m_stageData = new StageData(Vector3.zero, Quaternion.identity);

                return m_stageData;
            }
            set { m_stageData = value; }
        }


        public string m_StageName { get; set; }
        public Dictionary<string, DataContainer> m_datas { get; private set; }

        public SceneData(string stageName)
        {
            m_StageName = stageName;
            m_datas = new Dictionary<string, DataContainer>();
        }
    }

    [System.Serializable]
    public class StageData
    {
        public bool m_disallowPlayer;
        public SerializedVector3 m_playerPos;
        public SerializedQuaternion m_playerRot;

        public bool OverridedLocation { get; private set; }
        public bool NewEntry { get; private set; }

        public StageData(Vector3 playerPos, Quaternion playerRot)
        {
            m_playerPos = Serializer.Serializevector3(playerPos);
            m_playerRot = Serializer.SerializeQuaternion(playerRot);
            m_disallowPlayer = false;
            NewEntry = true;
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
            RegisterPlayerLocation(transform.position, transform.rotation);
        }

        public void RegisterPlayerLocation(Vector3 pos, Quaternion rot)
        {
            if (OverridedLocation)
            {
                OverridedLocation = false;
            }
            else
            {
                m_playerPos = Serializer.Serializevector3(pos);
                m_playerRot = Serializer.SerializeQuaternion(rot);
                
            }
            NewEntry = false;
        }


        /// <summary>
        /// Translate to a serializable form the given position and rotation
        /// to register the last player location of this stage
        /// </summary>
        public void OverridePlayerLocation(Vector3 pos, Quaternion rot)
        {
            m_playerPos = Serializer.Serializevector3(pos);
            m_playerRot = Serializer.SerializeQuaternion(rot);
            NewEntry = false;
            OverridedLocation = true;
        }


    }
}
