using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class BulletMove : MonoBehaviour
    {
        public float speed = 50f;
        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, 2.5f);
        }

        // Update is called once per frame
        void Update()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            // transform.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
            
            
        }
    }
}