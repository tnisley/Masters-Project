using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyIdleState : EnemyState
{
    protected float idleTime = 0f;  // 0 = no exit time
    protected bool flipOnExit = false; // should enemy turn around
    protected bool idleTimeOver = false;

    public EnemyIdleState(Enemy enemy, StateMachine stateMachine, string animBool) : base(enemy, stateMachine, animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
        if (flipOnExit)
            enemy.FlipSprite();

        idleTime = 0f; // reset time
        flipOnExit = false;  // reset value
        idleTimeOver = false;
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (idleTime > .001f && Time.time > startTime + idleTime)
            idleTimeOver = true;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public void SetIdleTime(float time)
    {
        idleTime = time;
    }

    public void SetFlipOnExit()
    {
        flipOnExit = true;
    }
}
