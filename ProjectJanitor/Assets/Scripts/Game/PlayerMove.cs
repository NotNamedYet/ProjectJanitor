using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class PlayerMove : MonoBehaviour
    {
        public float speed = 10f;

        // Update is called once per frame
        void Update()
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += move * speed * Time.deltaTime;
        }
    }

}