using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "ScriptableObject/Enemy/EnemyData")]

public class EnemyData : ScriptableObject
{
    public int contactDamage = 1;

    public float movementSpeed = 3f;

    public bool canBeDamaged = true;    // can the enemy be damaged?

    public bool useGround = true;  //Does enemy interact with ground?

    public bool usePlatforms = false;  //Does enemy interact with one-way platforms?

    // How far to detect walls
    public float wallDistance = .1f;

    // How far to check for edge of platform. Set higher if player can drop.
    public float edgeDistance = .1f;

    public float rearEdgeDistance = .2f;

    public float attackDistance = 0;

    public float minCloseRangeDistance;

    public float maxCloseRangeDistance;


    public bool startWhenInView;

    public LayerMask whatIsTarget;

}
