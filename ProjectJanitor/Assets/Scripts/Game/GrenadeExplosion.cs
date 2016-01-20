using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public class GrenadeExplosion : MonoBehaviour
    {
        public ParticleSystem explosion;
        // Use this for initialization
        void Start()
        {
            explosion = gameObject.GetComponent<ParticleSystem>();
            Destroy(gameObject, explosion.duration);
        }

        // Update is called once per frame
        void Update()
        {

        }
    } 
}
