using UnityEngine;
using System.Collections;

namespace GalacticJanitor.Game
{
    public class BossLeachCollider : MonoBehaviour
    {
        public AlienBoss m_boss;
        public LayerMask m_colliderMask;
        public int m_leachAmount;
        public bool onAttack;

        // Use this for initialization
        void Start()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (onAttack)
            {
                if ((m_colliderMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
                {
                    if (other.CompareTag("BossCocoon"))
                    {
                        BossCocoon cocoon = other.GetComponent<BossCocoon>();
                        m_boss.Heal(cocoon.LifeLeach(m_leachAmount * 10));
                    }
                    else
                    {
                        LivingEntity entity = other.GetComponent<LivingEntity>();
                        entity.TakeDirectDamage(m_leachAmount);
                        m_boss.Heal(entity.LastDamage);
                    }
                }
            }
        }
    } 
}
