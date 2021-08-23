using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : Enemy
{
    //componenets
    Physics physics;

    //State machine
    StateMachine stateMachine;

    public ScorpionAttackState attackState;
    public ScorpionEvadeState evadeState;
    public ScorpionIdleState idleState;
    public ScorpionPatrolState patrolState;
    public BasicEnemyDeathState deathState;

    public Data_EnemyProjectileAttackState attackData;
    public Data_EnemyPatrolState patrolData;
    public Data_EnemyEvadeState evadeData;
    


    public override void AnimationFinished()
    {
        base.AnimationFinished();
    }

    public override void FlipSprite()
    {
        base.FlipSprite();
    }

    public override bool IsAtEdge()
    {
        return base.IsAtEdge();
    }

    public override bool IsAtWall()
    {
        return base.IsAtWall();
    }

    protected override void Activate()
    {
        base.Activate();

        stateMachine.Initialize(patrolState);
    }

    protected override void Awake()
    {
        base.Awake();

        physics = GetComponent<Physics>();

        stateMachine = new StateMachine();

        idleState = new ScorpionIdleState(this, stateMachine, "idle", this);
        patrolState = new ScorpionPatrolState(this, stateMachine, "walk", patrolData, this);
        evadeState = new ScorpionEvadeState(this, stateMachine, "evade", evadeData, this);
        attackState = new ScorpionAttackState(this, stateMachine, "attack", attackData, this);
        deathState = new BasicEnemyDeathState(this, stateMachine, "dead");


        stateMachine.AddState(idleState);
        stateMachine.AddState(patrolState);
        stateMachine.AddState(evadeState);
        stateMachine.AddState(attackState);
        stateMachine.AddState(deathState);

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isActive)
        {
            stateMachine.CurrentState.StateFixedUpdate();

            //move left/right; no movement if falling
            if (physics.IsGrounded())
            {
                Vector2 move = velocity;
                move.x *= -transform.right.x;
                physics.AddVelocity(move);
            }
        }
    }

    protected override void Update()
    {
        if (isActive)
        {
            stateMachine.CurrentState.StateUpdate();
            Debug.Log("Scorpion state = " + stateMachine.CurrentState.ToString());
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    protected override void SetGround()
    {
        base.SetGround();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        base.Die();
        physics.gravityEnabled = false;
    }


}
