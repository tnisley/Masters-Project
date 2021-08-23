using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : AirState
{
    public PlayerFallState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
    {
        // listen for jump input
        InputHandler.OnJumpPressed.AddListener(HandleJumpInput);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Entered fall state");
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

        //player has landed
        if (player.physics.IsGrounded() || player.isOnPlatform)
        {
            if (player.input.XInput != 0)
                stateMachine.ChangeState(player.moveState);
            else
                stateMachine.ChangeState(player.idleState);
        }
    }

    // player can jump a few frames after falling.
    // Improves responsiveness
    // Else, get time jump is pressed to process after touching ground
    protected void HandleJumpInput()
    {
        
        if (stateMachine.CurrentState == this)
        {
            Debug.Log("Jump called from fall state");
            if (Time.time - startTime < player.lateJumpAllowed
                && stateMachine.PreviousState != player.jumpState)
                stateMachine.ChangeState(player.jumpState);
            else
                jumpTime = Time.time;
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        InputHandler.OnJumpPressed.RemoveListener(HandleJumpInput);
    }

}
