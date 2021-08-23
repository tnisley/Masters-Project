using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAttackState : EnemyState
{
    public Data_EnemyProjectileAttackState attackData;
    public bool timeOver;
    public bool hasAttacked;

    public EnemyProjectileAttackState(Enemy enemy, StateMachine stateMachine, string animBool, Data_EnemyProjectileAttackState attackData) : base(enemy, stateMachine, animBool)
    {
        this.attackData = attackData;
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        timeOver = false;
        hasAttacked = false;

        if (attackData.attackAfterAnimation == false)
        {
            enemy.projectileWeapon.UseWeapon();
            hasAttacked = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();

        if (isAnimationOver && attackData.attackAfterAnimation == true && !hasAttacked)
        {
            enemy.projectileWeapon.UseWeapon();
            hasAttacked = true;
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (Time.time - startTime >= attackData.stateDuration && hasAttacked)
            timeOver = true;
    }

    protected override void CheckTransitions()
    {
        base.CheckTransitions();
    }
}
