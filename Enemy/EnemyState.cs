using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : IState
{
    protected Enemy enemy;
    protected StateMachine stateMachine;
    protected float startTime; // time state was entered
    protected string animBool; //bool value to set for animator
    protected bool isAnimationOver = false;

    public EnemyState()
    {
    }

    // Constructor for state with animation
    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBool)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBool = animBool;

        enemy.animEvents.OnAnimationFinished.AddListener(AnimationFinished);
    }

    //Constructor for state with no animation
    public EnemyState(Enemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBool = null;
    }

    public virtual void OnEnter()
    {
        startTime = Time.time;

        if (animBool != null)
            enemy.animator.SetBool(animBool, true);  //start animation

    }

    public virtual void OnExit()
    {
        if (animBool != null)
            enemy.animator.SetBool(animBool, false);  // stop animation
        isAnimationOver = false;
    }

    public virtual void StateFixedUpdate()
    { }

    public virtual void StateUpdate()
    {
        CheckTransitions();
    }


    // To be implemented by derived class
    protected virtual void CheckTransitions()
    { }

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
        if (animBool != null)
            enemy.animEvents.OnAnimationFinished.RemoveListener(AnimationFinished);
    }

}
