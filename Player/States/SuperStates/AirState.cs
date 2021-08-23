using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerActionState
{
    public AirState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
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
        
        // add horizontal movement
        if (player.input.XInput != 0)
            player.physics.AddVelocity(new Vector2(player.input.XInput * player.MovementSpeed, 0f));
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        // Enter ladder
        if (player.input.YInput != 0 && player.isInLadderZone && !player.isAttacking)
        {
            bool aboveLadder = player.boxCollider.IsAbove(player.currentLadder); ;

            // press down if above ladder, otherwise press up
            if (!aboveLadder && player.input.YInput > 0)
                stateMachine.ChangeState(player.climbState);
        }
    }


}
