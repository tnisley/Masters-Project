using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBlockScanState : EnemyScanForTargetState
{
    ShootingBlock block;

    public ShootingBlockScanState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyScanForTargetState scanData, ShootingBlock block) : base(enemy, stateMachine, animBool, scanData)
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
        base.CheckTransitions();
        if (isTargetInRange)
            stateMachine.ChangeState(block.attackState);
    }
}
