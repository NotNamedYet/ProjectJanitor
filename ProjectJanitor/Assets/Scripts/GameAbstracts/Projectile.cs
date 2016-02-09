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

        void Awake()
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


            CheckCollision(moveDistance); // Is something there ? ...
            transform.Translate(Vector3.forward * moveDistance); //... Nope ! Run baby run !
        }

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
        protected abstract void OnHit(RaycastHit hit);
    } 
}
