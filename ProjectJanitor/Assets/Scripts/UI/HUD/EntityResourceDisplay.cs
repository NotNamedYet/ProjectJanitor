using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;

namespace GalacticJanitor.UI
{
    public class EntityResourceDisplay : DisplayComponent
    {

        public ProgressBar healthBar;
        public ProgressBar energyBar;

        public void UpdateState(LivingEntity entity)
        {
            if (healthBar) healthBar.UpdateProgress(entity.m_entity.health, entity.m_entity.maxHealth);
            if (energyBar) energyBar.UpdateProgress(entity.m_entity.armor, entity.m_entity.maxArmor);
        }
        
    } 
}
