using UnityEngine;
using System.Collections;

public interface IDamageable {

    void TakeDirectDamage(int damage, bool ignoreArmor);

    void TakeDirectDamage(int damage);

    void Die();

    
}
