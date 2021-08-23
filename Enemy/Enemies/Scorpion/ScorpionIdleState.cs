using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionIdleState : EnemyIdleState
{
    protected Scorpion scorpion;

    public ScorpionIdleState(Enemy enemy, StateMachine stateMachine, string animBool, Scorpion scorpion) : base(enemy, stateMachine, animBool)
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
        if (idleTimeOver)
            stateMachine.ChangeState(scorpion.patrolState);

    }
}
