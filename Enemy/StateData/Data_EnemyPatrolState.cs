using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PatrolStateData", menuName = "ScriptableObject/Enemy/StateData/PatrolStateData")]


public class Data_EnemyPatrolState : ScriptableObject
{
    public float walkSpeed;

    public float idleAtEdgeTime;
}
