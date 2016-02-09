using UnityEngine;
using System.Collections;
using System;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{

    public class AmmoBox : Entity
    {
        [Header("Container Elements")]
        private MarinesType marineType;

        [Tooltip("Bullet = 0, Grenade and fuel = 1")]
        public AmmoType ammoType;

        [Range(0, 300)]
        public int amount = 0;

        [Tooltip("Check it if you want a random number, use min and maxRangeToRandom")]
        public bool useRandomAmount;
        public int minRangeToRandom;
        public int maxRangeToRandom;

        [Header("Render")]
        public SpriteRenderer visual;
        [Tooltip("Put here sprites you want use to")] // 3 sprites for the moment
        public Sprite bulletBox;
        public Sprite grenadeBox;
        public Sprite flamethrowerBox;

        protected override void Start()
        {
            base.Start();
            marineType = GameController.Player.marinesType;
            RenderSprite();
            if (useRandomAmount) amount = MakeRandomAmount(minRangeToRandom, maxRangeToRandom);
            LoadData();
        }

        void RenderSprite()
        {
            if (ammoType == AmmoType.AmmoType0)
            {
                visual.sprite = bulletBox;
            }
            else
            {
                if (marineType == MarinesType.MajCarter) visual.sprite = flamethrowerBox;

                else visual.sprite = grenadeBox;
            }
        }

        public int MakeRandomAmount(int min, int max)
        {
            int result = UnityEngine.Random.Range(min - 1, max + 1);
            return result;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("I'am a AmmoBox type " + ammoType + " and i was touched by " + marineType);

                PlayerAmmo playerInv = other.gameObject.GetComponent<PlayerAmmo>();
                playerInv.PickUpAmmo(ammoType, amount);
                amount = 0;
                Depop();
            }
        }

        /// <summary>
        /// Save and Destroy this Object
        /// </summary>
        void Depop()
        {
            SaveObject();
            Destroy(gameObject);
        }

        public override ObjectData CreateData()
        {
            AmmoBoxData data = new AmmoBoxData();
            data.canSpawn = amount > 0;
            data.UniqueId = UniqueId;
            data.amount = amount;
            data.RegisterPosition(transform.position, transform.rotation);

            return data;
        }

        public override void LoadData()
        {
            AmmoBoxData data = SaveSystem.GetObjectData(UniqueId) as AmmoBoxData;

            if (data != null)
            {
                amount = data.amount;
            }
        }
    }   
}

public enum AmmoType
{
    AmmoType0 = 0, AmmoType1
}

[Serializable]
public class AmmoBoxData : ObjectData
{
    public int amount;
}