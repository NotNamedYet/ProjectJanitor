using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;
using MonoPersistency;

namespace GalacticJanitor.Game
{
    public abstract class Entity : MonoPersistent
    {
        [Header("Entity")]
        public string entityDisplayName;


        public static implicit operator bool(Entity check)
        {
            return check != null;
        }
    } 
}
