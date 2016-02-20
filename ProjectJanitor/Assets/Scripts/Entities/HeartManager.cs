using UnityEngine;
using System.Collections;
using System;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{

    public class HeartManager : ResourceLoot
    {
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
    }
}
