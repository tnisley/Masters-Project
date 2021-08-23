using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes player transparent and prevents damage while in this state

public class PlayerTempInvulnerableState : PlayerDamageState
{

    public PlayerTempInvulnerableState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.canBeDamaged = false;
        player.spriteRenderer.SetAlpha(.75f);
    }

    public override void OnExit()
    {
        base.OnExit();
        player.canBeDamaged = true;
        player.spriteRenderer.SetAlpha(1f);

    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        // Flicker effect
        if (Time.frameCount % 2 == 0)

            player.spriteRenderer.SetAlpha(.6f);
        else
            player.spriteRenderer.SetAlpha(.3f);


        // Alert player invulnerability is ending
        if (Time.time - startTime > player.invulnerabilityDuration - .25f)
            player.spriteRenderer.SetAlpha(1f);


        // back to normal state after invulnerability ends
        if (Time.time - startTime > player.invulnerabilityDuration)
            stateMachine.ChangeState(player.normalDamageState);
    }

}
