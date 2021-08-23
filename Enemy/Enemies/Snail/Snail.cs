using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    #region State Machine

    StateMachine stateMachine;

    // States
    public SnailIdleState idleState;
    public SnailWalkState walkState;
    public BasicEnemyDeathState deathState;

    // State data. Set references in inspector.
    public Data_EnemyIdleState idleData;
    public Data_EnemyWalkState walkData;

    #endregion

    SimpleMoveController moveController;
    Vector2 move;

    protected override void Awake()
    {
        base.Awake();
        moveController = GetComponent<SimpleMoveController>();

        stateMachine = new StateMachine();

        // Create states
        idleState = new SnailIdleState(this, stateMachine, "idle", this);
        walkState = new SnailWalkState(this, stateMachine, "walk", walkData, this);
        deathState = new BasicEnemyDeathState(this, stateMachine, "dead");
        stateMachine.AddState(idleState);
        stateMachine.AddState(walkState);
        stateMachine.AddState(deathState);

    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isActive)
        {
            stateMachine.CurrentState.StateUpdate();
        }
    }

    protected override void FixedUpdate()
    {
        if (isActive)
        {
            stateMachine.CurrentState.StateFixedUpdate();
            move = velocity;
            move.x *= -transform.right.x;
            moveController.Move(move);
        }
    }

    protected override void Activate()
    {
        base.Activate();
        stateMachine.Initialize(walkState);
    }

    private void OnDestroy()
    {
        stateMachine.Destroy();
    }
}
