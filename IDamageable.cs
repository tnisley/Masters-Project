using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface for any entity that can take damage

public interface IDamageable
{
    void TakeDamage(DamageData damage);
}
