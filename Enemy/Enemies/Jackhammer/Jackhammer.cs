using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jackhammer : Enemy
{
    StateMachine stateMachine;
    public JackhammerWalkState walkState;
    public Data_EnemyWalkState walkData;
    SimpleMoveController moveController;
    Vector2 move;


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
        stateMachine.Initialize(walkState);
    }

    protected override void Awake()
    {
        base.Awake();

        moveController = GetComponent<SimpleMoveController>();

        stateMachine = new StateMachine();
        walkState = new JackhammerWalkState(this, stateMachine, "walk", walkData);
        stateMachine.AddState(walkState);
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
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isActive)
        {
            stateMachine.CurrentState.StateFixedUpdate();
            move = velocity;
            move.x *= -transform.right.x;
            moveController.Move(move);
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

    private void OnDestroy()
    {
        stateMachine.Destroy();
    }


}
