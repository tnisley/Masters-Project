using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WalkStateData", menuName = "ScriptableObject/Enemy/StateData/WalkStateData")]

public class Data_EnemyWalkState : ScriptableObject
{
    public float walkSpeed;

    public float idleAtEdgeTime;
}
