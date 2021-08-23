using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvadeState : EnemyState
{
    protected Data_EnemyEvadeState data;

    protected bool isAtEdge = false;
    protected bool isAtWall = false;
    protected bool targetInMaxCloseRange = false;

    public EnemyEvadeState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyEvadeState data) : base(enemy, stateMachine, animBool)
    {
        this.data = data;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        targetInMaxCloseRange = true;
        enemy.SetVelocityX(-data.walkSpeed);
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
        if (enemy.isAtEdgeBehind())
            isAtEdge = true;
        if (enemy.isAtWallBehind())
            isAtWall = true;

        if (Time.frameCount % 4 == 0)
        {
                targetInMaxCloseRange = enemy.isTargetInMaxCloseRange();
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    protected override void CheckTransitions()
    {
    }
}
