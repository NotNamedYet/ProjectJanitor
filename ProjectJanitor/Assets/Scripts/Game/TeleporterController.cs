using GalacticJanitor.Engine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GalacticJanitor.Game
{
    [ExecuteInEditMode]
    public class TeleporterController : MonoBehaviour
    {
        public TeleporterVisual telepoterVisual;

        [Space(8)]
        public bool active = true;
        public bool delayedActivation;
        public float delay;
        

        [Space(8)]
        public bool isLocal;
        [Header("If Local")]
        public TeleporterController localDestination;
        [Range(1, 15)]
        public float cooldownAfterJump = 1;
        public float frozenAfterJump;
        [Header("If Interscene")]
        public string sceneDestinationName;

        bool stayOff;
        float cooldown = 0;


        void Start()
        {
            if (delayedActivation)
            {
                AddCooldown(delay);
            }

            if (isLocal && localDestination == null)
            {
                Debug.LogError("Local " + gameObject.name + " with no Linked destination");
            }
        }



        void Update()
        {
            if (isLocal && localDestination != null)
                Debug.DrawLine(transform.position, localDestination.transform.position, Color.cyan);

            if (cooldown > 0)
                cooldown -= Time.deltaTime;

            if (cooldown > 0 && !stayOff)
                SetAsleep(true);

            if (cooldown <= 0 && stayOff)
            {
                SetAsleep(false);
                if (cooldown < 0) cooldown = 0;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!active) return;

            if (!stayOff)
            {
                if (other.gameObject.tag == "Player")
                {
                    if (isLocal)
                    {
                        if (localDestination != null)
                        {
                            localDestination.AddCooldown(cooldownAfterJump);

                            if (frozenAfterJump > 0)
                            {
                                PlayerController frozen = other.GetComponent<PlayerController>();
                                if (frozen != null) frozen.MovementCooldown = frozenAfterJump;
                            }

                            LocalTeleport(other.gameObject, localDestination.transform);
                        }
                    }
                    else
                    {
                        GameController.LoadScene(sceneDestinationName);
                    }
                }
            }
        }

        void SetAsleep(bool toggle)
        {
            stayOff = toggle;

            if (telepoterVisual != null)
            {
                telepoterVisual.SetAsleep(toggle);
            }
        }

        public void AddCooldown(float amount)
        {
            cooldown += amount;
            SetAsleep(true);
        }

        void LocalTeleport(GameObject obj, Transform targetPoint)
        {
            float x = targetPoint.position.x;
            float y = obj.transform.position.y;
            float z = targetPoint.position.z;

            obj.transform.position = new Vector3(x, y, z);
        }
    } 
}

