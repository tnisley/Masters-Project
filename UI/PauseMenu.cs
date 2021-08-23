using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Controls the pause menu

public class PauseMenu : MonoBehaviour
{
    public enum State { PAUSED, RESTART, QUIT }

    State currentState;

    [SerializeField]
    GameObject textBox;
    TextMeshProUGUI text;

    string pauseText = "Press 'R' to Restart level, \n 'Q' to Quit game.";
    string restartText = "Are you sure you want to restart? Y/N";
    string quitText = "Are you sure you want to quit? Y/N";

    private void OnEnable()
    {
        currentState = State.PAUSED;
        text = textBox.GetComponent<TextMeshProUGUI>();
        text.text = pauseText;


    }

    private void Update()
    {
        switch (currentState)
        {
            case State.PAUSED:
                if (Input.GetKeyDown(KeyCode.R))
                    ChangeState(State.RESTART);
                else if (Input.GetKeyDown(KeyCode.Q))
                    ChangeState(State.QUIT);
                break;

            case State.QUIT:
                if (Input.GetKeyDown(KeyCode.Y))
                    GameEvents.OnQuitGame.Invoke();
                else if (Input.GetKeyDown(KeyCode.N))
                    ChangeState(State.PAUSED);
                break;

            case State.RESTART:
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    Time.timeScale = 1f;
                    GameEvents.OnRestartLevel.Invoke();
                }
                else if (Input.GetKeyDown(KeyCode.N))
                    ChangeState(State.PAUSED);
                break;
        }
    }

    private void ChangeState(State newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case State.PAUSED:
                text.text = pauseText;
                break;

            case State.RESTART:
                text.text = restartText;
                break;

            case State.QUIT:
                text.text = quitText;
                break;


        }
    }
}
