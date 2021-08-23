using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : PlayerActionState
{
    float stateDuration = .5f;  // how long player is damaged/stunned
    Vector2 verticalKnockback;
    Vector2 horizontalKnockback;
    int knockbackDirection = 1;

    public PlayerKnockbackState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
    {
        horizontalKnockback = new Vector2(player.knockback.x, 0);
        verticalKnockback = new Vector2(0, player.knockback.y);
        GameEvents.OnPlayerDamage.AddListener(SetToCurrentState);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.physics.ResetVelocity(); // reset player momentum
        if (stateMachine.PreviousState != player.climbState)
        {
            // Apply vertical knockback
            player.physics.AddVelocity(verticalKnockback);
        }

        // set horizontal knockback direction
        if (player.transform.position.x - player.enemyCollision.transform.position.x > 0)
            knockbackDirection = 1;
        else
            knockbackDirection = -1;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();

        // add horizontal knockback
        // due to velocity calculations, horizontal knockback must be added each frame
        // to move the player
        if (stateMachine.PreviousState != player.climbState && !player.physics.IsGrounded())
            player.physics.AddVelocity(horizontalKnockback * knockbackDirection);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        // exit damage state and return player control
        if (Time.time - startTime > stateDuration)
            stateMachine.ChangeState(player.fallState);
    }

    public override void Destroy()
    {
        base.Destroy();
        GameEvents.OnPlayerDamage.RemoveListener(SetToCurrentState);
    }
}
