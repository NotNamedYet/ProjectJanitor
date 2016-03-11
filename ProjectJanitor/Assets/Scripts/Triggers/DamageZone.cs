using UnityEngine;
using System.Collections;
using GalacticJanitor.Game;

[RequireComponent(typeof(Collider))]
public class DamageZone : MonoBehaviour {

    public int m_DamagePerSecond;
    public bool m_IgnoreArmor;
    public bool m_IsEntraving;
    public int m_EntraveDuration;

    bool onCoolDown;
    IDamageable playerDamageComponent;

    void ReleaseCooldown()
    {
        onCoolDown = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (onCoolDown || !other.CompareTag("Player"))
            return;

        if (playerDamageComponent == null)
            playerDamageComponent = other.GetComponent(typeof(IDamageable)) as IDamageable;

        playerDamageComponent.TakeDirectDamage(m_DamagePerSecond, m_IgnoreArmor);

        if (m_IsEntraving)
            other.GetComponent<PlayerController>().Entrave(m_EntraveDuration);

        onCoolDown = true;
        Invoke("ReleaseCooldown", 1.1f);
    }
}
