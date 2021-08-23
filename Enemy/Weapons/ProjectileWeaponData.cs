using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ProjectileWeaponStateData", menuName = "ScriptableObject/Enemy/WeaponData/ProjectileWeaponData")]

public class ProjectileWeaponData : ScriptableObject
{
    public int maxProjectiles = 1;

    public Vector2 projectileVelocity;

}
