using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Normal damage state. Has no actions itself, 
// but transitions to other damage states.

public class PlayerNormalDamageState : PlayerDamageState
{
    public PlayerNormalDamageState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
        GameEvents.OnPlayerDamage.AddListener(EnterTempInvulnerableState);
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
    }

    public override void StateUpdate()
    {
        base.StateFixedUpdate();
    }

    private void EnterTempInvulnerableState()
    {
        if (stateMachine.CurrentState == this)
            stateMachine.ChangeState(player.tempInvulnerableState);
        Debug.Log("Changed to invulnerable state");
    }

    public override void Destroy()
    {
        base.Destroy();
        GameEvents.OnPlayerDamage.RemoveListener(EnterTempInvulnerableState);

    }

}
