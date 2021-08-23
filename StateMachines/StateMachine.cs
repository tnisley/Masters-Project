using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; private set; }
    public IState PreviousState { get; private set; }
    public List<IState> states;


    public StateMachine()
    {
        states = new List<IState>();
    }

    public void Initialize(IState initialState)
    {
        CurrentState = initialState;
        PreviousState = initialState;
        CurrentState.OnEnter();
    }

    public void ChangeState(IState newState)
    {
        if (states.Contains(newState))
        {
            CurrentState.OnExit();
            PreviousState = CurrentState;
            CurrentState = newState;
            CurrentState.OnEnter();
        }

        else
            Debug.LogError("State machine does not contain the state \"" + newState.ToString() + "\"");

    }

    public void AddState(IState state)
    {
        states.Add(state);
    }

    public void Destroy()
    {
        foreach (IState state in states)
            state.Destroy();
    }
}
