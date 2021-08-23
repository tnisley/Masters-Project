using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Emptystate with no actions or animations for an inactive enemy

public class EnemyInactiveState : EnemyState
{
    public EnemyInactiveState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
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
    }

    
}
