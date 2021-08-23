using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionAttackState : EnemyProjectileAttackState
{
    Scorpion scorpion;

    public ScorpionAttackState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyProjectileAttackState attackData, Scorpion scorpion) : base(enemy, stateMachine, animBool, attackData)
    {
        this.scorpion = scorpion;
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
            scorpion.idleState.SetIdleTime(attackData.attackDelay);
            stateMachine.ChangeState(scorpion.idleState);
        }
    }
}
