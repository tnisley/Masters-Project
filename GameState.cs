using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum State { ACTIVE, PAUSED, MENU, DEAD };

    public State currentState { get; private set; }

    [SerializeField]
    State initialState;

    private void Awake()
    {
        currentState = initialState;
        InputHandler.OnPauseButton.AddListener(delegate { ChangeGameState(State.PAUSED); });
        InputHandler.OnUnpauseButton.AddListener(delegate { ChangeGameState(State.ACTIVE); });
        GameEvents.OnPlayerDeath.AddListener(delegate { ChangeGameState(State.DEAD); });

    }

    public void ChangeGameState(State newState)
    {
        currentState = newState;
    }

    private void OnDestroy()
    {
        InputHandler.OnPauseButton.RemoveListener(delegate { ChangeGameState(State.PAUSED); });
        InputHandler.OnUnpauseButton.RemoveListener(delegate { ChangeGameState(State.ACTIVE); });
        GameEvents.OnPlayerDeath.RemoveListener(delegate { ChangeGameState(State.DEAD); });
    }
}
