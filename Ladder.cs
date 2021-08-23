using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ladder class detects if player is in ladder zone
// and trigger OnLadderZone game event

public class Ladder : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Check if player is in ladder trigger zone
    // and invoke event
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
                GameEvents.OnPlayerEnterLadderZone.Invoke(boxCollider);
    }

    // Check if player left ladder zone and invoke event
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            GameEvents.OnPlayerExitLadderZone.Invoke();
    }


}
