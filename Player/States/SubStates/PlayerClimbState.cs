using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerActionState
{
    private float animationSpeed;

    public PlayerClimbState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
    {
        // Listen for jump input
        InputHandler.OnJumpPressed.AddListener(Jump);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.physics.ResetVelocity();
        player.physics.gravityEnabled = false;
        float initialMovement = player.input.YInput * .25f;
        player.rbody.position = (new Vector2(player.currentLadder.gameObject.transform.position.x, player.rbody.transform.position.y + initialMovement));
    }

    public override void OnExit()
    {
        base.OnExit();
        player.physics.gravityEnabled = true;
        player.animator.enabled = true;
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        Debug.Log("FIXED UPDATE");

        if (player.input.YInput != 0)
        {
            player.animator.enabled = true;
            movement.Climb();
        }

        else player.animator.enabled = false;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log("UPDATE");

        // check for top of ladder while climbing
        if (player.input.YInput > 0 && player.currentLadder == null/*player.boxCollider.IsAbove(player.currentLadder)*/)
        {
            stateMachine.ChangeState(player.idleState);
        }

        // reached top of ladder or fell off bottom
        if (!player.isInLadderZone)
            stateMachine.ChangeState(player.idleState);

        // reached ground at bottom of ladder
        else if (player.physics.IsGrounded())
        {
            stateMachine.ChangeState(player.idleState);
        }

    }

    // jump off ladder
    protected void Jump()
    {
        // don't jump if still climbing up
        if (stateMachine.CurrentState == this && player.input.YInput <= 0)
        {
            // jump
            if (player.input.XInput != 0)
                stateMachine.ChangeState(player.jumpState);
           
            // fall
            else
            {
                // add downward momentum
                player.physics.AddVelocity(new Vector2(0, -2));
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        InputHandler.OnJumpPressed.RemoveListener(Jump);

    }

}
