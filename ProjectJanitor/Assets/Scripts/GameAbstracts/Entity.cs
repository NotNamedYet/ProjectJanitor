using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{
    public abstract class Entity : PersistentObject
    {

        [Header("Entity")]
        public string entityDisplayName;
        public bool allowOrderInHierachy = true;

        protected virtual void Start()
        {
            if (allowOrderInHierachy) transform.SetParent(GameController.EntityHolder);
        }
    } 
}
