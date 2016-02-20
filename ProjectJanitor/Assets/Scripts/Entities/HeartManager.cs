using UnityEngine;
using System.Collections;
using System;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{

    public class HeartManager : ResourceLoot
    {
        public float m_spinDistance;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(SpinRoutine());
        }

        protected override void OnLoot(PlayerController entity)
        {
            if (entity.Heal(amount))
            {
                Destroy(gameObject);
                GameController.NotifyPlayer("Health +" + amount, Color.green, 2);
            }

            else
            {
                GameController.NotifyPlayer("Health full", Color.green, 2);
            }
        }

        IEnumerator SpinRoutine()
        {
            PlayerController player = GameController.Player;

            while (true)
            {
                if (player && Vector3.Distance(player.transform.position, transform.position) < m_spinDistance)
                {
                    Spin();
                }

                yield return new WaitForEndOfFrame();
            }
        }

        void Spin()
        {
            transform.Rotate(0, 150 * Time.deltaTime, 0);
        }
    }
 
}
