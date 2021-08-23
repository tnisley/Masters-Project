using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public bool isActive = false;
    bool playerInDoor;

    // Start is called before the first frame update
    void Awake()
    {
        GameEvents.OnAllGemsCollected.AddListener(Activate);
        InputHandler.OnUpPressed.AddListener(PlayerExit);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            playerInDoor = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            playerInDoor = false;
    }

    void Activate ()
    {
        isActive = true;
        GetComponent<Animator>().SetBool("active", true);
    }

    void PlayerExit()
    {
        if (isActive && playerInDoor)
        {
            Debug.Log("Player exiting");
            GameEvents.OnPlayerExitLevel.Invoke();
        }
    }

    private void OnDestroy()
    {
        GameEvents.OnAllGemsCollected.RemoveListener(Activate);
        InputHandler.OnUpPressed.RemoveListener(PlayerExit);
    }
}
