using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{

    public class AlienMoveUnderGrid : MonoBehaviour
    {

        [Tooltip("Make ref in the editor with the game object \"Arrival point\".")]
        public Transform arrivalPoint;

        [Tooltip("Make ref in the editor with the alien's rig.")]
        public Animator rig;

        public float speed = 10;

        // Use this for initialization
        void Start()
        {
            gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            runBabie();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Arrival point") Destroy(gameObject);
        }

        void runBabie()
        {
            transform.LookAt(arrivalPoint);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            rig.SetFloat("magnitude", 1);
        }
    }

}