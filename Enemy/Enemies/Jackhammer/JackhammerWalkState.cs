using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackhammerWalkState : EnemyWalkState
{
    public JackhammerWalkState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyWalkState data) : base(enemy, stateMachine, animBool, data)
    {
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
            enemy.FlipSprite();
            isAtWall = false;
            isAtEdge = false;
        }

    }

}
