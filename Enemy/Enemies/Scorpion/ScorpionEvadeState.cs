using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionEvadeState : EnemyEvadeState
{
    protected Scorpion scorpion;

    public ScorpionEvadeState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyEvadeState data, Scorpion scorpion) : base(enemy, stateMachine, animBool, data)
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

        if (isAtEdge || isAtWall)
        {
            stateMachine.ChangeState(scorpion.attackState);
        }

        else if (!targetInMaxCloseRange)
            stateMachine.ChangeState(scorpion.patrolState);
    }
}
