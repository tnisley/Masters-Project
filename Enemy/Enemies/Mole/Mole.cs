using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Enemy
{

    StateMachine stateMachine = new StateMachine();

    // States
    public MoleActiveState activeState;
    public BasicEnemyDeathState deathState;

    protected override void Awake()
    {
        base.Awake();

        activeState = new MoleActiveState( this, stateMachine, "active", this);
        deathState = new BasicEnemyDeathState(this, stateMachine, "dead");
        stateMachine.AddState(activeState);
        stateMachine.AddState(deathState);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        boxCollider.enabled = false;

    }

    // Enable collider (called in animator)
    public void EnableCollider()
    {
        boxCollider.enabled = true;
    }

    // Disable collider (called in animator)
    public void DisableCollider()
    {
        boxCollider.enabled = false;
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
        }
    }

    protected override void Activate()
    {
        base.Activate();
        stateMachine.Initialize(activeState);
    }

    private void OnDestroy()
    {
        stateMachine.Destroy();
    }
}
