using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : EnemyState
{
    protected Data_EnemyWalkState data;

    protected bool isAtEdge= false;
    protected bool isAtWall = false;

    public EnemyWalkState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyWalkState data) : base(enemy, stateMachine, animBool)
    {
        this.data = data;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.SetVelocityX(data.walkSpeed);
    }

    public override void OnExit()
    {
        base.OnExit();
        enemy.SetVelocityX(0);
        isAtEdge = false;
        isAtWall = false;
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (enemy.IsAtEdge())
            isAtEdge = true;
        if (enemy.IsAtWall())
            isAtWall = true;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    protected override void CheckTransitions()
    {
    }
}
