using UnityEngine;
using System.Collections.Generic;

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
            RunBabieRun();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Arrival point") KillBabie();
        }

        void RunBabieRun()
        {
            transform.LookAt(arrivalPoint);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            rig.SetFloat("magnitude", 1);
        }

        void KillBabie()
        {
            GetComponentInParent<AlienPatrolUnderGridManager>().patrol.Remove(this);
            if (GetComponentInParent<AlienPatrolUnderGridManager>().patrol.Count == 0)
            {
                Destroy(GetComponentInParent<AlienPatrolUnderGridManager>().gameObject);
            }

            Destroy(gameObject);
        }
    }

}