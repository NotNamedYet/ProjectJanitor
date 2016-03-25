using UnityEngine;
using System.Collections;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{

    [RequireComponent(typeof(Collider))]
    public class SaveNotificationToPlayer : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameController.NotifyPlayer("Don't forget to save, janitor !", Color.red, 3);
            }
        }

    } 

}
