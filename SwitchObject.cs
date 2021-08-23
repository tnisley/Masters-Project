using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Switch used to open a door. Access from player controller.

public class SwitchObject : MonoBehaviour, iInteractive
{
    public enum SwitchType { DOOR, CONVEYOR, GRAVITY };
    public SwitchType switchType;

    public GameObject door;
    public bool canActivate = true; // can the switch be activated

    public bool On;  // current switch status
    public Sprite switchOn;
    public Sprite switchOff;


    // Flips the switch, then deactivates interaction. Call from player controller.
    public void FlipSwitch()
    {
        // switch the sprite
        if (!On)
            GetComponent<SpriteRenderer>().sprite = switchOn;
        else
            GetComponent<SpriteRenderer>().sprite = switchOff;
        On = !On; // update switch state

        // switch action
        switch (switchType)
        {
            case SwitchType.CONVEYOR:
                GameEvents.OnReverseConveyors.Invoke();
                break;

            case SwitchType.GRAVITY:
                GameEvents.OnFlipGravity.Invoke();
                break;

            case SwitchType.DOOR:
                // Switch no longer operable
                GetComponent<BoxCollider2D>().enabled = false;
                door.GetComponent<DoorObject>().OpenDoor();
                break;

        } 
    }

    public void Activate()
    {
        FlipSwitch();
    }
}
