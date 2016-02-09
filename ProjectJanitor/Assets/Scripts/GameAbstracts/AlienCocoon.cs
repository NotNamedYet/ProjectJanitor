using UnityEngine;
using System.Collections.Generic;

namespace GalacticJanitor.Game
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(SphereCollider))]
    public class AlienCocoon : MonoBehaviour
    {
        public CocoonState currentState = CocoonState.READY;
        SphereCollider objectCollider;
        LivingEntity entity;

        public Transform[] spawnLocations;
        public AlienBase typeOfAlien;
        public int maximumAlienByCycle;
        public float timeBetweenPop = 3f;
        public int maximumCycle = 1;
        public float cycleCooldown;
        public bool oneTimeAggro = false;

        float nextPop;
        float nextCycleTime;
        int currentCycle = 0;
        int counter = 0;
        int nextSpawn = 0;

        Transform target;

        void Awake()
        {
            objectCollider = GetComponent<SphereCollider>();
            objectCollider.isTrigger = true;
            entity = GetComponent<LivingEntity>();
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

            if (currentCycle >= maximumCycle)
            {
                entity.Kill(true);
            }

            if (currentState == CocoonState.COOLDOWN)
            {
                if (Time.time > nextCycleTime)
                {
                    NewCycle();
                }
            }
            else if (currentState == CocoonState.POP)
            {
                if (Time.time > nextPop)
                {
                    DoPop();
                    nextPop = Time.time + timeBetweenPop;
                }
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (currentState == CocoonState.READY)
                {
                    target = other.gameObject.transform;
                    currentState = CocoonState.POP;
                }
            }
        }

        Transform GetSpawnLoc()
        {
            if (spawnLocations.Length > 0)
            {
                Transform retval = spawnLocations[nextSpawn];
                nextSpawn++;
                if (nextSpawn >= spawnLocations.Length) nextSpawn = 0;
                return retval;
            }

            return gameObject.transform;
        }

        void DoPop()
        {

            if (counter < maximumAlienByCycle)
            {
                Transform loc = GetSpawnLoc();
                AlienBase pop = Instantiate(typeOfAlien, loc.position, loc.rotation) as AlienBase;
                pop.target = target;
                counter++;
            }
            else
            {
                currentState = CocoonState.COOLDOWN;
                nextCycleTime = Time.time + cycleCooldown;
            }
        }

        void NewCycle()
        {
            counter = 0;
            currentCycle++;

            if (!oneTimeAggro) currentState = CocoonState.READY;
            else currentState = CocoonState.POP;
        }
    } 

    public enum CocoonState
    {
        POP, SLEEP, READY, COOLDOWN
    }
}
