using UnityEngine;
using System.Collections;
using System;
using GalacticJanitor.Game;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Persistency
{
    [RequireComponent(typeof(AlienBase))]
    public class SavableAlien : SavableObject
    {

        private AlienBase alien;

        // Use this for initialization
        void Start()
        {
            alien = GetComponent<AlienBase>();
            RestoreObjectState();
        }

        protected override ObjectData BuildData()
        {
            AlienData data = new AlienData(base.uniqueId);
            data.positionData = Serializer.Serializevector3(alien.transform.position);
            data.rotationData = Serializer.SerializeQuaternion(alien.transform.rotation);
            data.healthData = alien.entity.health;
            data.armorData = alien.entity.armorPoint;
            data.alienNameData = alien.AlienName;
            if (!alien.entity.alive)
            {
                data.canSpawn = false;
            }
            return data;
        }

        protected override void RestoreObjectState()
        {
            AlienData source = LoadDataFromRegistery() as AlienData;
            if (source != null)
            {
                Vector3 pos = Serializer.DeserializeVector3(source.positionData);
                Quaternion rot = Serializer.DeserializeQuaternion(source.rotationData);

                if (alien == null) alien = GetComponent<AlienBase>();

                alien.transform.position = pos;
                alien.transform.rotation = rot;
                alien.entity.health = source.healthData;
                alien.entity.armorPoint = source.armorData;
            }
        }
    }

    [Serializable]
    public class AlienData : ObjectData
    {
        public SerializedVector3 positionData;
        public SerializedQuaternion rotationData;
        public int healthData;
        public int armorData;
        public string alienNameData;

        public AlienData(string uniqueId) : base(uniqueId){}

        public override string ToString()
        {
            return base.uniqueId + " Alien-" + alienNameData + " : " + healthData + "HP";
        }
    }
}
