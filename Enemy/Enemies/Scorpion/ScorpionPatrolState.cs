using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionPatrolState : EnemyPatrolState
{
    Scorpion scorpion;

    public ScorpionPatrolState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyPatrolState data, Scorpion scorpion) : base(enemy, stateMachine, animBool, data)
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
        if (playerInCloseRange)
            stateMachine.ChangeState(scorpion.evadeState);
        else if (playerInAttackRange)
            stateMachine.ChangeState(scorpion.attackState);
        else if (isAtWall || isAtEdge)
        {
            Debug.Log("Scorpion is at wall: " + isAtWall);
            Debug.Log("scorpion is at edge: " + isAtEdge);
            scorpion.idleState.SetIdleTime(data.idleAtEdgeTime);
            scorpion.idleState.SetFlipOnExit();
            stateMachine.ChangeState(scorpion.idleState);
        }

    }
}
