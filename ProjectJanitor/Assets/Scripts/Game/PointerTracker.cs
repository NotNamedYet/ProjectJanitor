using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    //[ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class PointerTracker : MonoBehaviour
    {

        // Another beautiful comment...
        public static Vector3 MousePosition { get; private set; }
        Camera mainCam;
        public Transform target;
        public float targetTrackingSpeed = 25f;
        public float trackingBoostRange = 5f;

        // Use this for initialization
        void Start()
        {
            if (target == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("Player");
                if (go != null)
                    target = go.transform;
            }
            mainCam = GetComponent<Camera>();
        }

        void FixedUpdate()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            FollowPlayer();

            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane virtualGround = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (virtualGround.Raycast(ray, out rayDistance))
            {
                MousePosition = ray.GetPoint(rayDistance);
                Debug.DrawLine(ray.origin, MousePosition, Color.red);
            }
        }

        void FollowPlayer()
        {
            if (target != null)
            {
                float speed = targetTrackingSpeed;
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                if (distanceToTarget > trackingBoostRange)
                {
                    speed *= distanceToTarget * 0.2f;
                }

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, 10, target.position.z), speed * Time.deltaTime);
            }      
        }
    }

}