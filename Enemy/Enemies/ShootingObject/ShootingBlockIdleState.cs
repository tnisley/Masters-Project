using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBlockIdleState : EnemyIdleState
{
    ShootingBlock block;

    public ShootingBlockIdleState(Enemy enemy, StateMachine stateMachine, string animBool, ShootingBlock block) : base(enemy, stateMachine, animBool)
    {
        this.block = block;
    }

    public override void Destroy()
    {
        base.Destroy();
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
            stateMachine.ChangeState(block.scanState);
    }
}
