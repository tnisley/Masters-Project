using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inherited by all grounded player states

public class GroundState : PlayerActionState
{
    public GroundState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
    {
        // Listen for jump input
        InputHandler.OnJumpPressed.AddListener(EnterJump);
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // Process jump buffer
        if (startTime - jumpTime < player.earlyJumpAllowed &&
            !player.isAttacking)
            stateMachine.ChangeState(player.jumpState);
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

        // falling
        if (!player.physics.IsGrounded() && !player.isOnPlatform)
            stateMachine.ChangeState(player.fallState);
        
        // Enter ladder
        if (player.input.YInput != 0 && player.isInLadderZone && !player.isAttacking)
        {
            bool aboveLadder = player.boxCollider.IsAbove(player.currentLadder);

            // press down if above ladder, otherwise press up
            if ((aboveLadder && player.input.YInput < 0) ||
                 (!aboveLadder && player.input.YInput > 0))
            {
                stateMachine.ChangeState(player.climbState);
                Debug.Log("Y input: " + player.input.YInput);
            }
        }

    }


    // Event triggered state changes
    protected void EnterJump()
    {
        if (stateMachine.CurrentState == this)
            stateMachine.ChangeState(player.jumpState);
    }

    public override void Destroy()
    {
        base.Destroy();
        InputHandler.OnJumpPressed.RemoveListener(EnterJump);
    }

}
