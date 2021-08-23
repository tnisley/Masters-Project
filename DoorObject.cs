using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for opening a door. Open door should be called
// from the door's corresponding switch.

public class DoorObject : MonoBehaviour
{
    public Sprite openDoorSprite;

    //Open the door
    public void OpenDoor()
    {
        GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
