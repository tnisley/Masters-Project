using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDamageState : IState
{

    protected Player player;
    protected StateMachine stateMachine;
    protected float startTime;

    protected PlayerDamageState(Player player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        GameEvents.OnPlayerDamage.AddListener(SetToCurrentState);

    }

    public virtual void OnEnter()
    {
        startTime = Time.time;
    }

    public virtual void OnExit()
    {
    }

    public virtual void StateFixedUpdate()
    {
    }

    public virtual void StateUpdate()
    {
    }

    // Sets this state to the current state. Used in conjunction with events.
    protected void SetToCurrentState()
    {
        stateMachine.ChangeState(this);
    }

    public virtual void Destroy()
    {
        GameEvents.OnPlayerDamage.RemoveListener(SetToCurrentState);
    }
}
