using GalacticJanitor.Game;
using System;
using System.Collections;
using UnityEngine;

namespace GalacticJanitor.Persistency
{
    [RequireComponent(typeof(AmmoBox))]
    public class SavableAmmoBox : SavableObject
    {

        private AmmoBox box;

        // Use this for initialization
        void Start()
        {
            box = gameObject.GetComponent<AmmoBox>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override ObjectData GetData()
        {
            AmmoBoxData data = new AmmoBoxData(uniqueId);
            data.amountInBox = box.amount;
            if (box.amount <= 0)
            {
                data.canSpawn = false;
            }
            return data;
        }

        public override void SetData(ObjectData data)
        {
            AmmoBoxData bridge = data as AmmoBoxData;
            if (bridge != null)
            {
                box.amount = bridge.amountInBox;
            }
        }
    }

    [Serializable]
    public class AmmoBoxData : ObjectData
    {
        public int amountInBox;
        public AmmoBoxData(string uniqueId) : base(uniqueId) { }

        public override string ToString()
        {
            return base.uniqueId + " Container : " + amountInBox;
        }
    } 
}
