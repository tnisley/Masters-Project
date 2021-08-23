using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{

    protected Data_EnemyPatrolState data;

    protected bool isAtEdge = false;
    protected bool isAtWall = false;
    protected bool playerInAttackRange = false;
    protected bool playerInCloseRange = false;

    public EnemyPatrolState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyPatrolState data) : base(enemy, stateMachine, animBool)
    {
        this.data = data;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerInAttackRange = enemy.isTargetInAttackRange();
        playerInCloseRange = enemy.isTargetInMinCloseRange();
        enemy.SetVelocityX(data.walkSpeed);
    }

    public override void OnExit()
    {
        base.OnExit();
        enemy.SetVelocityX(0);
        isAtEdge = false;
        isAtWall = false;
        playerInAttackRange = false;
        playerInCloseRange = false;
}

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (enemy.IsAtEdge())
            isAtEdge = true;
        if (enemy.IsAtWall())
            isAtWall = true;

        if (Time.frameCount % 4 == 0)
        {
            playerInAttackRange = enemy.isTargetInAttackRange();
            playerInCloseRange = enemy.isTargetInMinCloseRange();
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
