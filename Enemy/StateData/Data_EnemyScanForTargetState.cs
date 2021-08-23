using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScanStateData", menuName = "ScriptableObject/Enemy/StateData/ScanStateData")]

public class Data_EnemyScanForTargetState : ScriptableObject
{
    public int scanInterval = 2;
}
