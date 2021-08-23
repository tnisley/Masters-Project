using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : GroundState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
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

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();

        //Don't move when attacking on ground
        if (!player.isAttacking)
            movement.MoveHorizontal();
        else
            movement.ApplyFriction();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        // player stops moving
        if (player.input.XInput == 0)
            stateMachine.ChangeState(player.idleState);
    }
}
