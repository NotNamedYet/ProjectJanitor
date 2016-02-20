using UnityEngine;
using System.Collections;
using System;
using GalacticJanitor.Engine;

namespace GalacticJanitor.Game
{
    public class ArmorManager : ResourceLoot
    {
        protected override void OnLoot(PlayerController entity)
        {
            if (entity.RepairArmor(amount))
            {
                Destroy(gameObject);
                GameController.NotifyPlayer("Armor +" + amount, Color.green, 2);
            }

            else
            {
                GameController.NotifyPlayer("Armor full", Color.green, 2);
            }
        }
    }

}