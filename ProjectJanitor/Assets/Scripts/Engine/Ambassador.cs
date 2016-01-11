using UnityEngine;
using System.Collections.Generic;
using GalacticJanitor.Persistency;

namespace GalacticJanitor.Engine
{
    public class Ambassador : MonoBehaviour
    {

        [SerializeField]
        public List<Savable> savables;

        void Awake()
        {
            savables = new List<Savable>();

            Savable[] s = FindObjectsOfType<Savable>();
            savables.AddRange(s);
        }



        public void Register(Savable s)
        {
            if (!savables.Contains(s))
            {
                savables.Add(s);
            }
        }
    } 
}
