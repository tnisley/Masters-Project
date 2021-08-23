using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for a basic ball that travels in a straight line
// and spins as it moves.

[RequireComponent (typeof (Animator))]
public class BasicBall : Projectile
{
    public string spinRightBool, spinLeftBool;
    Animator animator;

    protected override void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
    }
    protected override void LaunchAction()
    {
        if (targetVelocity.x > 0)
        {
            animator.SetBool(spinLeftBool, true);
            animator.SetBool(spinRightBool, false);
        }
        else if (targetVelocity.x < 0)
        {
            animator.SetBool(spinLeftBool, false);
            animator.SetBool(spinRightBool, true);
        }
    }
}
