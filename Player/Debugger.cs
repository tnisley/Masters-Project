using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Debugger : MonoBehaviour
{
    Player player;
    Physics physics;
    SpriteRenderer s;
    public GameObject debugMessage;


    private void Start()
    {
        player = GetComponent<Player>();
        physics = player.GetComponent<Physics>();
        s = player.GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        // Toggle Gravity
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1) && GameEvents.OnGravityChange != null)
            GameEvents.OnGravityChange.Invoke(Physics.gravity.NORMAL);


        else if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2) && GameEvents.OnGravityChange != null)
            GameEvents.OnGravityChange.Invoke(Physics.gravity.LOW);


        else if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3) && GameEvents.OnGravityChange != null)
        {
            {
                Debug.Log("Reverse gravity!");
                GameEvents.OnGravityChange.Invoke(Physics.gravity.REVERSE);
            }
        }

        // Toggle god-mode
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha0))
        {
            player.canBeDamaged = !player.canBeDamaged;
            debugMessage.SetActive(!debugMessage.activeInHierarchy);
        }
    }
}

