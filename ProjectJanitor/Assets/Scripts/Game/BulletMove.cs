using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class BulletMove : MonoBehaviour
    {
        public float speed = 500f;

        [Tooltip("Set time to destroy the gameobject if no collide")]
        public float destroyTime = 2.5f;

        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, destroyTime);
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Wall" || other.tag == "Alien" || other.tag == "Indestructible Box") // Not finish
            {
                Debug.Log("Im a bullet and i touch : " + other.tag.ToString() + ", i must be dead now");
                Destroy(gameObject);
            }
                
        }
    }
}