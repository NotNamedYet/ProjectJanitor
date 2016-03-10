using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;
using MonoPersistency;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{
    public abstract class Entity : MonoPersistent
    {
        [Header("Entity")]
        public string entityDisplayName;

    } 
}
