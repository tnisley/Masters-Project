using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ProjectileAttackStateData", menuName = "ScriptableObject/Enemy/StateData/ProjectileAttackStateData")]

public class Data_EnemyProjectileAttackState : ScriptableObject
{
    public float attackDelay = 2f;

    public float stateDuration = 1f;

    // Should animation finish before attack is started
    public bool attackAfterAnimation = false;

    public GameObject projectile;

}
