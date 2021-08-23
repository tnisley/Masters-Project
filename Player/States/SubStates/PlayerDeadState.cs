using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerActionState
{
    public PlayerDeadState(Player player, StateMachine stateMachine, string animBool) : base(player, stateMachine, animBool)
    {
        InputHandler.OnDeadKeyPress.AddListener(ResetLevel);
        GameEvents.OnPlayerDeath.AddListener(SetToCurrentState);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //player.SetDead();
    }

    public override void OnExit()
    {
        //base.OnExit();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public void ResetLevel()
    {
        if (isAnimationOver)
            GameEvents.OnRestartLevel.Invoke();
    }

    public override void Destroy()
    {
        base.Destroy();
        Debug.Log("Dead State Being Destroyed");
        InputHandler.OnDeadKeyPress.RemoveListener(ResetLevel);
        GameEvents.OnPlayerDeath.RemoveListener(SetToCurrentState);
    }

}
