using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Contains static game events that can be called from
//  any class

public class GameEvents: MonoBehaviour
{
    #region Player Related Events
    //Call when player jumps
    public static UnityEvent OnPlayerJump = new UnityEvent();

    // call when player switches weapons
    public static WeaponDataEvent OnWeaponChange = new WeaponDataEvent();

    // Called at start of attack
    public static UnityEvent OnPlayerAttackBegin = new UnityEvent();

    // called at end of attack
    public static UnityEvent OnPlayerAttackEnd = new UnityEvent();

    // called when player dies
    public static UnityEvent OnPlayerDeath = new UnityEvent();

    // called when player enters/leaves ladder zone
    public static ColliderEvent OnPlayerEnterLadderZone = new ColliderEvent();
    public static UnityEvent OnPlayerExitLadderZone = new UnityEvent();

    // called when player takes damage
    public static UnityEvent OnPlayerDamage = new UnityEvent();

    // Called when player tries to go through the level exit
    public static UnityEvent OnPlayerExitLevel = new UnityEvent();

    // Player gets collectible object
    public static CollectibleEvent OnGetCollectible = new CollectibleEvent();


    #endregion

    //event to change gravity
    public static GravityEvent OnGravityChange = new GravityEvent();

    // called when a gravity switch object is activated
    public static UnityEvent OnFlipGravity = new UnityEvent();

    // event to reverse conveyor belts
    public static UnityEvent OnReverseConveyors = new UnityEvent();

    // UI Events
    public static UIEvent OnPlayerHealthChange = new UIEvent();

    public static UIEvent OnScoreUpdate = new UIEvent();

    public static UIEvent OnGemsUpdate = new UIEvent();

    public static UnityEvent OnAllGemsCollected = new UnityEvent();


    // Menu Events
    public static UnityEvent OnQuitGame = new UnityEvent();

    public static UnityEvent OnRestartLevel = new UnityEvent();

    // Send damage message to a gameobject
    public static void SendDamageMessage(GameObject target, DamageData damage)
    {
        target.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
    }

}

// Class for event that has a gravity state parameter
public class GravityEvent : UnityEvent<Physics.gravity> { }

// Class for event that has a collider2d parameter
public class ColliderEvent : UnityEvent<Collider2D> { }

// Class for event that has a gameobject parameter
public class GameObjectEvent : UnityEvent<GameObject> { }

// Class for event that has a weapon data parameter
public class WeaponDataEvent : UnityEvent<WeaponData> { }

// class for event that takes a collectible object as a parameter
public class CollectibleEvent : UnityEvent<CollectibleData> { }

// class for UIEvents
public class UIEvent : UnityEvent<int> { }

