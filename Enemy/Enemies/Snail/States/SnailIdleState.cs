using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailIdleState : EnemyIdleState
{
    Snail snail;

    public SnailIdleState(Enemy enemy, StateMachine stateMachine, string animBool, Snail snail) : base(enemy, stateMachine, animBool)
    {
        this.snail = snail;
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
        base.StateUpdate();
        
    }

    protected override void CheckTransitions()
    {
        if (idleTimeOver)
            stateMachine.ChangeState(snail.walkState);
    }
}
