using UnityEngine;
using System.Collections;
using System;

namespace GalacticJanitor.Engine
{

    public class Serializer
    {

        public static SerializedVector3 Serializevector3(Vector3 source)
        {
            SerializedVector3 sv = new SerializedVector3();
            sv.x = source.x;
            sv.y = source.y;
            sv.z = source.z;
            return sv;
        }

        public static Vector3 DeserializeVector3(SerializedVector3 source)
        {
            return new Vector3(source.x, source.y, source.z);
        }

        public static SerializedVector2 Serializevector2(Vector2 source)
        {
            SerializedVector2 sv = new SerializedVector2();
            sv.x = source.x;
            sv.y = source.y;
            return sv;
        }

        public static Vector2 DeserializeVector2(SerializedVector2 source)
        {
            return new Vector2(source.x, source.y);
        }

        public static SerializedQuaternion SerializeQuaternion(Quaternion source)
        {
            SerializedQuaternion sq = new SerializedQuaternion();
            sq.x = source.x;
            sq.y = source.y;
            sq.z = source.z;
            sq.w = source.w;
            return sq;
        }

        public static Quaternion DeserializeQuaternion(SerializedQuaternion source)
        {
            return new Quaternion(source.x, source.y, source.z, source.w);
        }
    }

    [Serializable]
    public struct SerializedVector3
    {
        public float x;
        public float y;
        public float z;

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }
    }

    [Serializable]
    public struct SerializedVector2
    {
        public float x;
        public float y;

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }
    }

    [Serializable]
    public struct SerializedQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})", x,y,z,w);
        }
    }

}
