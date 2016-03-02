using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{
    //[ExecuteInEditMode]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Camera))]
    public class TopDownCamera : MonoBehaviour
    {

        // Another beautiful comment...
        public static Vector3 MousePosition { get; private set; }
        Camera mainCam;
        public float targetTrackingSpeed = 25f;
        public float trackingBoostRange = 5f;
        public bool trackPlayerOnStart = true;
        public bool fixedTarget;

        private Transform target;

        public void SetTarget(Transform target)
        {
            if (target == null || !fixedTarget)
            {
                this.target = target;
            }
        }

        void Awake()
        {
            mainCam = GetComponent<Camera>();
            GameController.TopDownCamera = this;
        }

        void Start()
        {
            if (trackPlayerOnStart)
            {
                SetTarget(GameController.Player.transform);
                JumpToTarget();
            }
        }
        
        // Update is called once per frame
        void Update()
        {
            FollowTarget();
            UpdatePointer();
        }


        #region Target
        void FollowTarget()
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

        void UpdatePointer()
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane virtualGround = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (virtualGround.Raycast(ray, out rayDistance))
            {
                MousePosition = ray.GetPoint(rayDistance);
                Debug.DrawLine(ray.origin, MousePosition, Color.red);
            }
        }

        public void JumpToTarget()
        {
            if (target)
            {
                transform.position = new Vector3(target.position.x, 10, target.position.z);
            }
        }
        #endregion

    }

}