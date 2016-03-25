using UnityEngine;
using System.Collections;
using System;
using GalacticJanitor.Engine;
using MonoPersistency;

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

        void Awake()
        {
            if (useRandomAmount) amount = MakeRandomAmount(minRangeToRandom, maxRangeToRandom);
        }

        void Start()
        {
            marineType = GameController.Player.marinesType;
            RenderSprite();
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
            int result = UnityEngine.Random.Range(min, max + 1);
            return result;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                //Debug.Log("I'am a AmmoBox type " + ammoType + " and i was touched by " + marineType);

                PlayerAmmo playerInv = other.gameObject.GetComponent<PlayerAmmo>();

                if (ammoType == AmmoType.AmmoType1 && marineType == MarinesType.MajCarter) playerInv.PickUpAmmo(ammoType, amount * 20); // Flamethrower
                else playerInv.PickUpAmmo(ammoType, amount);
                amount = 0;
                Depop();
            }
        }

        /// <summary>
        /// Save and Destroy this Object
        /// </summary>
        void Depop()
        {
            Save();
            Destroy(gameObject);
        }

        public override void CollectData(DataContainer container)
        {
            container.Addvalue("amount", amount);
            container.m_spawnable = amount > 0;
        }

        public override void LoadData(DataContainer container)
        {
            amount = container.GetValue<int>("amount");
        }
    }   
}

public enum AmmoType
{
    AmmoType0 = 0, AmmoType1
}
