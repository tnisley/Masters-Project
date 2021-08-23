using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanForTargetState : EnemyState
{
    protected bool isTargetInRange = false;
    protected Data_EnemyScanForTargetState scanData;

    public EnemyScanForTargetState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyScanForTargetState scanData) : base(enemy, stateMachine, animBool)
    {
        this.scanData = scanData;
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        isTargetInRange = enemy.isTargetInAttackRange();

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void StateFixedUpdate()
    { 
        base.StateFixedUpdate();
        if (Time.frameCount % scanData.scanInterval == 0)
            isTargetInRange = enemy.isTargetInAttackRange();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    protected override void CheckTransitions()
    {
        base.CheckTransitions();
    }
}
