using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds data that is passed to an object when doing damage

public struct DamageData
{
    public GameObject gameObject;
    public int dmgAmount;

    public DamageData(GameObject gameObject, int dmgAmount)
    {
        this.gameObject = gameObject;
        this.dmgAmount = dmgAmount;
    }
}
