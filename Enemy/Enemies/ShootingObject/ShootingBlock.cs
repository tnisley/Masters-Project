using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBlock : Enemy
{
    StateMachine stateMachine = new StateMachine();

    // States
    public ShootingBlockIdleState idleState;
    public ShootingBlockScanState scanState;
    public ShootingBlockAttackState attackState;
    public EnemyInactiveState inactiveState;

    public Data_EnemyScanForTargetState scanData;
    public Data_EnemyProjectileAttackState attackData;


    public override void AnimationFinished()
    {
        base.AnimationFinished();
    }

    public override void FlipSprite()
    {
        base.FlipSprite();
    }

    protected override void Activate()
    {
        base.Activate();
        stateMachine.Initialize(scanState);

    }

    protected override void Awake()
    {
        base.Awake();
        idleState = new ShootingBlockIdleState(this, stateMachine, "idle", this);
        scanState = new ShootingBlockScanState(this, stateMachine, "idle", scanData, this);
        attackState = new ShootingBlockAttackState(this, stateMachine, "attack", attackData, this);
        inactiveState = new EnemyInactiveState(this, stateMachine);

        stateMachine.AddState(inactiveState);
        stateMachine.AddState(idleState);
        stateMachine.AddState(scanState);
        stateMachine.AddState(attackState);

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isActive)
        {
            stateMachine.CurrentState.StateFixedUpdate();
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isActive)
        {
            stateMachine.CurrentState.StateUpdate();
            Debug.Log("Block state : " + stateMachine.CurrentState.ToString());
        }
    }

    private void OnBecameInvisible()
    {
        stateMachine.ChangeState(idleState);
    }

}
