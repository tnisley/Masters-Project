using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State player enters upon pressing jump

public class PlayerJumpState : AirState

{
    public PlayerJumpState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
    {
        // Cancel jump if jump button is released
        InputHandler.OnJumpReleased.AddListener(CancelJump);
    }

    // Add jump impulse upon entering state
    public override void OnEnter()
    {
        base.OnEnter();

        // Normal Jump
        if (stateMachine.PreviousState != player.climbState)
        {
            movement.Jump();
        }

        // Jump off climbable object
        else
        {
            movement.Jump(player.jumpOffVelocity);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        // Change to fall state if player is no longer moving up
        switch (player.physics.GravityState)
        {
            case Physics.gravity.REVERSE:
                if (player.physics.GetVelocity().y >= 0 && !player.isOnPlatform)
                    stateMachine.ChangeState(player.fallState);
                break;

            case Physics.gravity.NORMAL:
            case Physics.gravity.LOW:
                if (player.physics.GetVelocity().y < 0)
                    stateMachine.ChangeState(player.fallState);
                break;
        }
    }

    private void CancelJump()
    {
        Debug.Log("CancelJump called from " + stateMachine.CurrentState.ToString());
        if (stateMachine.CurrentState == this)
        {
            movement.CancelJump();
            Debug.Log("Jump released");
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        InputHandler.OnJumpReleased.RemoveListener(CancelJump);
    }

}
