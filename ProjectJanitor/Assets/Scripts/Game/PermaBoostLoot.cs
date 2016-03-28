using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;
using System;
using MonoPersistency;
using GalacticJanitor.Engine;

[RequireComponent(typeof(SphereCollider))]
public class PermaBoostLoot : PersistentLoot {

    public ResourceType m_ResourceType;
    public int m_amount = 50;
    public int m_Limit;
    public GameObject m_Rig;

    protected override bool OnLoot(PlayerController player)
    {

        if (m_ResourceType == ResourceType.HEALTH)
        {
            if (m_Limit > 0 && player.m_entity.maxHealth >= m_Limit)
                return false;

            player.m_entity.maxHealth += m_amount;
            GameController.NotifyPlayer("+" + m_amount.ToString() + " Max HP !", Color.green, 4);
        }
        else
        {
            if (m_Limit > 0 && player.m_entity.maxArmor >= m_Limit)
                return false;

            player.m_entity.maxArmor += m_amount;
            GameController.NotifyPlayer("+" + m_amount.ToString() + " Max Armor !", Color.green, 4);
        }
        player.UpdateDisplay();
        if (m_ResourceType == ResourceType.HEALTH) player.Heal();
        if (m_ResourceType == ResourceType.ARMOR) player.RepairArmor();
        return true;
    }

    public override void PlaySound()
    {
        if (m_Rig) m_Rig.SetActive(false);
        base.PlaySound();
    }
}

public enum ResourceType
{
    HEALTH,
    ARMOR
}
