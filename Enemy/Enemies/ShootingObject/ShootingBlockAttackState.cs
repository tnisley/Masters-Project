using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBlockAttackState : EnemyProjectileAttackState
{
    ShootingBlock block;

    public ShootingBlockAttackState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyProjectileAttackState attackData, ShootingBlock block) : base(enemy, stateMachine, animBool, attackData)
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
        if (timeOver)
        {
            block.idleState.SetIdleTime(attackData.attackDelay);
            stateMachine.ChangeState(block.idleState);
        }
    }
}
