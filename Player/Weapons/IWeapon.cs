using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface for a weapon

public interface IWeapon
{
    // Attack using weapon
    void UseWeapon();

    // Should be checked by attack controller calling UseWeapon()
    bool CanAttack();

    void PlayAttackAnimation();

    // Expose SetActive of game object in interface
    void SetActive(bool isActive);

}
