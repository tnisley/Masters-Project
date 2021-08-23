using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyState
{

    public EnemyDeathState(Enemy enemy, StateMachine stateMachine, string animBool) : base(enemy, stateMachine, animBool)
    {
        enemy.events.OnEnemyDeath.AddListener(SetToCurrentState);
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

    public override void Destroy()
    {
        base.Destroy();
        enemy.events.OnEnemyDeath.RemoveListener(SetToCurrentState);

    }


}