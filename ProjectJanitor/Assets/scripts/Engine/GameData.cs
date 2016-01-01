using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GalacticJanitor.Engine
{
    [Serializable]
    public struct GameData
    {
        public string fileName;
        public string currentSceneName;
        public long createdTime;

        public string playerName;
        public float playerHealth;
        public SerializedVector3 playerPos;
        public SerializedQuaternion playerRot;
    }
}
