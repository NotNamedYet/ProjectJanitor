using GalacticJanitor.Persistency;
using UnityEngine;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(SavableAlien))]
    [RequireComponent(typeof(LivingEntity))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AlienBase : MonoBehaviour
    {
        protected NavMeshAgent pathfinder;
        public LivingEntity entity;
        EnemyState state = EnemyState.IDLE;

        public string AlienName;
        public Transform sensor;
        public Animator rigging;
        public Transform target;

        [Header("Behavior", order = 0)]
        public float maxAttackRange;
        public float maxDistanceFromSpawn;
        float baseSpeed;

        [Header("Patrol", order = 1)]
        public bool patrol;
        public Transform[] patrolMilestones;
        private int patrolIndex;
        private short patrolSense;

        [Header("Attack behavior", order = 2)]
        public float attackPerSecond;
        public bool enraged;
        public int remainingHealthToEnrage;
        public int enrageModifier;

        private Vector3 spawn;

        void Start()
        {
            pathfinder = GetComponent<NavMeshAgent>();
            entity = GetComponent<LivingEntity>();
            baseSpeed = pathfinder.speed;
            spawn = transform.position;
        }

        void Update()
        {
            //Enrage state check.
            if (entity.health <= remainingHealthToEnrage)
            {
                Enrage();
            }
            else
            {
                Traquilize();
            }
            
            //sensor traking if target exists
            if (sensor!= null && target) sensor.LookAt(target);

            //Check EnemyState
            UpdateState();
        }

        void FixedUpdate()
        {
            CalculateBehavior();
        }

        /// <summary>
        /// Calculat the right action to do according to the object state.
        /// Physics dependant. -> FixedUpdate
        /// </summary>
        void CalculateBehavior()
        {
            if (state == EnemyState.FOLLOW)
            {
                pathfinder.SetDestination(target.position);
            }
            else if (state == EnemyState.RETURN)
            {
                pathfinder.SetDestination(spawn);
            }
            else if (state == EnemyState.ATTACK)
            {
                // Reduce the NavMeshAgent stopping range until target is reachable
                // or restore the value if target is close enought to be in sight
                if (TargetInSight())
                {
                    pathfinder.stoppingDistance = maxAttackRange;
                    SlerpToTarget();
                    Attack();
                }
                else
                {
                    pathfinder.stoppingDistance = pathfinder.stoppingDistance -= 1;
                    if (target != null) pathfinder.SetDestination(target.position);
                }
            }
        }

        /// <summary>
        /// Smoothly rotate the object in the direction of his target 
        /// </summary>
        void SlerpToTarget()
        {
            if (target != null)
            {
                Vector3 fixTargetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
                Quaternion quat = Quaternion.LookRotation(fixTargetPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, quat, 3f * Time.deltaTime);
            }
        }

        /// <summary>
        /// Determine if the target is in sight.
        /// </summary>
        /// <returns>True if nothing stand between this object and his target</returns>
        bool TargetInSight()
        {
            if (target == null) return false;

            Ray ray = new Ray(transform.position, target.position - transform.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.blue);

                if (hit.collider.transform == target)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Update the tacking state of this enemy.
        /// </summary>
        void UpdateState()
        {
            if (Vector3.Distance(transform.position, spawn) > maxDistanceFromSpawn || (!patrol && target == null && Vector3.Distance(transform.position, spawn) > 2))
            {
                state = EnemyState.RETURN;
                target = null;
            }
            else
            {
                if (target != null)
                {
                    if (GetDistanceFromTarget() < maxAttackRange)
                    {
                        state = EnemyState.ATTACK;
                    }
                    else
                    {
                        state = EnemyState.FOLLOW;
                    }
                }
                else
                {
                    if (patrol)
                    {
                        state = EnemyState.PATROL;
                    }
                    else
                    {
                        state = EnemyState.IDLE;
                        
                    }
                }
            }
        }

        public float GetDistanceFromTarget()
        {
            return Vector3.Distance(transform.position, target.position);
        }

        /// <summary>
        /// If not enraged yet, berserk it.
        /// </summary>
        void Enrage()
        {
            if (!enraged)
            {
                enraged = true;
                pathfinder.speed = baseSpeed * enrageModifier;
            }
        }

        /// <summary>
        /// Calm down... Good alien, good... 
        /// </summary>
        void Traquilize()
        {
            if (enraged)
            {
                enraged = false;
                pathfinder.speed = baseSpeed;
            }
        }

        /// <summary>
        /// 01100100 01101001 01100101 00100000 01100010 01101001 01110100 01100011 01101000 00100000 00100001 
        /// </summary>
        protected virtual void Attack(){}

    } 

    public enum EnemyState { ATTACK, FOLLOW, IDLE, RETURN, PATROL}

}
