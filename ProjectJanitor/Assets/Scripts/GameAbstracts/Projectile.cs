using GalacticJanitor.Engine;
using UnityEngine;

namespace GalacticJanitor.Game
{
    public abstract class Projectile : MonoBehaviour
    {
        public LayerMask colliderMask;
        public float maxLifetime;
        public int baseDamage;
        public float baseSpeed;
        public bool raycastCollide;

        /// <summary>
        /// Add speed to the base speed
        /// </summary>
        /// <param name="addition">The amount of speed to add</param>
        public void AddSpeed(float addition)
        {
            baseSpeed += addition;
        }

        /// <summary>
        /// Add damage to the base damage
        /// </summary>
        /// <param name="addition"></param>
        public void AddDamage(int addition)
        {
            baseDamage += addition;
        }

        protected virtual void Awake()
        {
            transform.SetParent(GameController.ProjectileHolder);
        }

        //Destroy the object at the end of his lifetime
        void Start()
        {
            Destroy(gameObject, maxLifetime); //Projecto morghulis ...
        }

        void Update()
        {
            // We need the distance to reach for this frame to determine the distance of the raycast colllision
            float moveDistance = baseSpeed * Time.deltaTime;

            if (raycastCollide)
                CheckCollision(moveDistance); // Is something there ? ...

            transform.Translate(Vector3.forward * moveDistance); //... Nope ! Run baby run !
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (!raycastCollide && (colliderMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            {
                OnHit(other);
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Check the collision, raycasting way
        /// </summary>
        /// <param name="distance">The distance to reach at the next frame</param>
        void CheckCollision(float distance)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, distance, colliderMask, QueryTriggerInteraction.Collide))
            {
                OnHit(hit);
            }
        }

        /// <summary>
        /// Perform this if the raycast hit somthing.
        /// </summary>
        /// <param name="hit">The raycast hit to check from</param>
        protected virtual void OnHit(RaycastHit hit) { }

        /// <summary>
        /// Perform this if the object collide with something.
        /// </summary>
        /// <param name="hit">The raycast hit to check from</param>
        protected virtual void OnHit(Collider collider) { }
    } 
}
