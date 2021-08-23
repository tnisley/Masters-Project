using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour, IMover
{
    public Animator[] animators;

    public float speed = 1;  // conveyor speed
    float animMultiplier = .8f; // conversion constant for animation speed -> velocity
    public int currentDirection = 1;  // 1 = CW, -1 = CCW  

    string animVariable = "Direction";

    private void Awake()
    {
        GameEvents.OnReverseConveyors.AddListener(ReverseDirection);
    }

    // Initialize movement
    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();

        foreach(Animator a in animators)
        {
            a.SetInteger(animVariable, currentDirection);
            a.speed = speed * animMultiplier;
        }

    }

    public Vector2 GetVelocity()
    {
        float xVelocity = speed * currentDirection;

        return new Vector2(xVelocity, 0);
    }

    public void ReverseDirection()
    {
        currentDirection *= -1;

        foreach (Animator a in animators)
        {
            a.SetInteger(animVariable, currentDirection);
        }
    }

    private void OnDestroy()
    {
        GameEvents.OnReverseConveyors.RemoveListener(ReverseDirection);

    }



}
