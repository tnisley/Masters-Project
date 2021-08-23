using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleActiveState : EnemyState
{
    Mole mole;

    public MoleActiveState(Enemy enemy, StateMachine stateMachine, string animBool, Mole mole) : base(enemy, stateMachine, animBool)
    {
        this.mole = mole;
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
