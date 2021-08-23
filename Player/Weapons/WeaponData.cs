using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "ScriptableObject/WeaponData/WeaponData")]

public class WeaponData : ScriptableObject
{
    public float attackDelay; // time between attacks

    public Sprite UISprite;  // holds sprite displayed in UI

    // TO DO: Add weapon SFX
}
