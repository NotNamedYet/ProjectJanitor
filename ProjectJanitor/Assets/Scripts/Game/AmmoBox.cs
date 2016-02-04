using UnityEngine;
using System.Collections;
using GalacticJanitor.Persistency;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(SavableAmmoBox))]
    public class AmmoBox : MonoBehaviour
    {

        SpriteRenderer spriteRenderer;
        MarinesType marineType;

        [Tooltip("Put here sprites you want use to")] // 3 sprites for the moment
        public Sprite[] sprites;

        [Tooltip("Bullet = 0, Grenade and fuel = 1")]
        public AmmoType ammoType;

        [Range(0, 300)]
        public int amount = 0;

        [Tooltip("Check it if you want a random number, use min and maxRangeToRandom")]
        public bool useRandomAmount;
        public int minRangeToRandom;
        public int maxRangeToRandom;

        private string uniqueID;

        // Use this for initialization
        void Start()
        {
            marineType = GalacticJanitor.Engine.GameController.Controller.currentDataLoader.playerType;
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            RenderSprite();

            if (useRandomAmount) amount = MakeRandomAmount(minRangeToRandom, maxRangeToRandom);
        }

        // Update is called once per frame
        void Update()
        {
            if (amount <= 0)
            {
                Destroy(gameObject);
            }
        }

        void RenderSprite()
        {
            if (ammoType == AmmoType.AmmoType0) GetSprite("spt_Container_Bullets");

            else
            {
                if (marineType == MarinesType.MajCarter) GetSprite("spt_Container_Flame");

                else GetSprite("spt_Container_Grenades");
            }
        }

        void GetSprite(string name)
        {
            foreach (Sprite spt in sprites)
            {
                if (spt.name == name) spriteRenderer.sprite = spt;
            }
        }

        public int MakeRandomAmount(int min, int max)
        {
            int result = Random.Range(min - 1, max + 1);
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
            }
        }
    }
    
    public enum AmmoType
    {
        AmmoType0 = 0, AmmoType1
    }
}
