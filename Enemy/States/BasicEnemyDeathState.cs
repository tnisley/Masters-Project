using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDeathState : EnemyDeathState
{
    public BasicEnemyDeathState(Enemy enemy, StateMachine stateMachine, string animBool) : base(enemy, stateMachine, animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.Die();
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
        if (isAnimationOver)
            GameObject.Destroy(enemy.gameObject);
    }

    protected override void CheckTransitions()
    {
        base.CheckTransitions();
    }

}
