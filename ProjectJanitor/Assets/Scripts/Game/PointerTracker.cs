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
        public Transform player;

        // Use this for initialization
        void Start()
        {
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
            if (player != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 10, player.position.z), 25f * Time.deltaTime);
            }      
        }
    }

}