using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(SphereCollider))]
    public class Teleporter : MonoBehaviour
    {

        public bool isArrival;
        public string targetScene;


        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (!isArrival)
                {
                    SceneManager.LoadScene(targetScene);
                } 
            }
        }
    } 
}
