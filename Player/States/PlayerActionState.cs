using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for states. Each state implements movement 
// for that state.

public abstract class PlayerActionState : IState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected PlayerMovement movement;
    protected Vector2 playerVelocity = new Vector2();

    protected bool hasAnimation;
    protected string animBool; //bool value to set for animator
    protected bool isAnimationOver = false;

    protected float startTime; // time state was entered
    protected static float jumpTime = Mathf.NegativeInfinity; // used to buffer jump

    // Constructor for a state without an animation
    public PlayerActionState(Player player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        hasAnimation = false;
        movement = player.movement;

        player.animEvents.OnAnimationFinished.AddListener(AnimationFinished);
    }

    // Constructor for a state with an animation
    public PlayerActionState(Player player, StateMachine stateMachine, string animBool) : this(player, stateMachine)
    {
        hasAnimation = true;
        this.animBool = animBool;
    }

    public virtual void OnEnter()
    {
        startTime = Time.time;
        if (hasAnimation)
            player.animator.SetBool(animBool, true);
    }

    public virtual void OnExit()
    {
        if (hasAnimation)
            player.animator.SetBool(animBool, false);
        isAnimationOver = false;


    }

    public virtual void StateUpdate()
    {
    }

    public virtual void StateFixedUpdate()
    {
    }

    // set animation over for current state
    // passes event to current state
    protected void AnimationFinished()
    {
        if (stateMachine.CurrentState == this)
            isAnimationOver = true;
    }

    // Sets this state to the current state. Used in conjunction with events.
    protected void SetToCurrentState()
    {
        stateMachine.ChangeState(this);
    }
 
    public virtual void Destroy()
    {
        Debug.Log("calling destructor in " + this.ToString());
        player.animEvents.OnAnimationFinished.RemoveListener(AnimationFinished);
    }
}
