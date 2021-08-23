using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailWalkState : EnemyWalkState
{
    Snail snail;

    public SnailWalkState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyWalkState data, Snail snail) : base(enemy, stateMachine, animBool, data)
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
        if (isAtEdge || isAtWall)
        {
            snail.idleState.SetIdleTime(data.idleAtEdgeTime);
            snail.idleState.SetFlipOnExit();
            stateMachine.ChangeState(snail.idleState);
        }
    }
}
