using UnityEngine;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(Collider))]
    public class FlameController : MonoBehaviour
    {
        [HideInInspector]
        public int flameDmg;

        private bool canDamageThisFrame;
        public float timer;
        private float timerActive; // Timer in game

        // public Dictionary<int, float> targetsBurning;
        public List<int> targetsBurning;
        // Use this for initialization
        void Start()
        {
            // targetsBurning = new Dictionary<int, float>();
            targetsBurning = new List<int>();
        }

        // Update is called once per frame
        void Update()
        {
            FlameCanDamageTimer();
        }

        private void FlameCanDamageTimer()
        {
            timerActive += Time.deltaTime;
            if (timerActive >= timer)
            {
                timerActive = 0f;
                targetsBurning.Clear();
            }
        }

        void OnTriggerStay(Collider other)
        {

            if (!targetsBurning.Contains(other.gameObject.GetInstanceID()) && other.GetComponent<LivingEntity>())
            {
                targetsBurning.Add(other.gameObject.GetInstanceID());
                other.GetComponent<LivingEntity>().TakeDirectDamage(DoDamage());
            }
        }

        int DoDamage()
        {
            return flameDmg;
        }
    }

}