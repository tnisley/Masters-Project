using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : GroundState
{
    public PlayerIdleState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    // Process state movement
    public override void StateFixedUpdate()
    {
        movement.ApplyFriction();
    }

    // Update the current state
    public override void StateUpdate()
    {
        base.StateUpdate();

        // move input
        if (player.input.XInput != 0)
            stateMachine.ChangeState(player.moveState);
    }
}
