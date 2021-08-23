using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Class to get player input. Provides simpler abstraction
// for Unity's built-in Input class.

public class InputHandler : MonoBehaviour
{


    [SerializeField]
    GameState gameState;

    // Invert axes
    bool isXInverted = false;
    bool isYInverted = false;

    // constants for determining direction
    public const int UP = 1;
    public const int DOWN = -1;
    public const int RIGHT = 1;
    public const int LEFT = -1;

    // Assign controller buttons to an alias
    //Active state
    private string jumpButton = "A_Button";
    private string attackButton = "X_Button";
    private string weaponChangeButton = "Y_Button";
    private string activateButton = "B_Button";
    private string pauseButton = "Start_Button";

    // Menu State
    private string confirm = "A_Button";


    // Raw axis values for joystick
    public float RawXInput { get; private set; }
    public float RawYInput { get; private set; }

    // Normalized axis values for joystick
    public int XInput { get; private set; }
    public int YInput { get; private set; }

    Vector2 axesLastFrame;

    // Buffering button presses
    float jumpTime;
    const float jumpBufffer = .06f;

    //Input events that get triggered

    // Game Active
    public static UnityEvent OnJumpPressed = new UnityEvent();
    public static UnityEvent OnJumpReleased = new UnityEvent();
    public static UnityEvent OnAttackPressed = new UnityEvent();
    public static UnityEvent OnAttackReleased = new UnityEvent();
    public static UnityEvent OnWeaponChangeButton = new UnityEvent();
    public static UnityEvent OnActivateButton = new UnityEvent();
    public static UnityEvent OnPauseButton = new UnityEvent();
    public static UnityEvent OnUpPressed = new UnityEvent();

    public static UnityEvent OnUnpauseButton = new UnityEvent();

    public static UnityEvent OnConfirmPressed = new UnityEvent();

    public static UnityEvent OnDeadKeyPress = new UnityEvent();


    protected void Start()
    {
        GameEvents.OnGravityChange.AddListener(InvertX);
    }

    // Set inputs using Unity's Input class
    private void Update()
    {
        switch (gameState.currentState)
        {
            case GameState.State.ACTIVE:
                SetActiveInput();
                break;

            case GameState.State.PAUSED:
                SetPauseInput();
                break;

            case GameState.State.MENU:
                SetMenuInput();
                break;

            case GameState.State.DEAD:
                SetDeadInput();
                break;

        }

    }

    // Functions to read each input
    private void SetAxes()
    {
        RawXInput = Input.GetAxisRaw("Horizontal");
        if (isXInverted)
            RawXInput *= -1;

        RawYInput = Input.GetAxisRaw("Vertical");
        if (isYInverted)
            RawYInput *= -1;

        // uGet normalized input values (0, 1 or -1)
        XInput = (int)(RawXInput * Vector2.right).normalized.x;
        YInput = (int)(RawYInput * Vector2.up).normalized.y;

        if (axesLastFrame.y != 1 && YInput == 1)
            OnUpPressed.Invoke();
        axesLastFrame.x = XInput;
        axesLastFrame.y = YInput;
    }

    // Sets the jump input
    private void SetJump()
    {
        if (Input.GetButtonDown(jumpButton))
        {
            OnJumpPressed.Invoke();
        }

        else if (Input.GetButtonUp(jumpButton))
        {
            Debug.Log("OnJumpReleased invoked");
            OnJumpReleased.Invoke();
        }
    }

    // Sets the attack input
    private void SetAttack()
    {
        if (Input.GetButtonDown(attackButton))
        {
            Debug.Log(" Attack Pressed!");
            OnAttackPressed.Invoke();
        }

        else if (Input.GetButtonUp(attackButton))
            OnAttackReleased.Invoke();
    }

    // Sets the jump input
    private void SetWeaponChange()
    {

        if (Input.GetButtonDown(weaponChangeButton))
        {
            Debug.Log("Weapon change input detected");
            OnWeaponChangeButton.Invoke();
        }
    }

    // Sets the Activate button
    private void SetActivate()
    {
        if (Input.GetButtonDown(activateButton))
        {
            Debug.Log("Activate input detected");
            OnActivateButton.Invoke();
        }
    }

    private void SetPause()
    {
        if (Input.GetButtonDown(pauseButton))
        {
            OnPauseButton.Invoke();
            Debug.Log("Pause pressed");
        }
    }

    // set active input
    void SetActiveInput()
    {
        SetAxes();
        SetJump();
        SetAttack();
        SetWeaponChange();
        SetActivate();
        SetPause();
    }

    void SetPauseInput()
    {
        XInput = 0;
        YInput = 0;
        if (Input.GetButtonDown(pauseButton))
            OnUnpauseButton.Invoke();
    }

    void SetMenuInput()
    {
        if (Input.GetButtonDown(confirm))
            OnConfirmPressed.Invoke();
    }

    void SetDeadInput()
    {
        XInput = 0;
        YInput = 0;
        if (Input.GetButtonDown(jumpButton) || Input.GetButtonDown(attackButton) ||
            Input.GetButtonDown(weaponChangeButton) || Input.GetButtonDown(activateButton) ||
            Input.GetButtonDown(pauseButton))
            OnDeadKeyPress.Invoke();
    }


    // Inverts the X axis
    private void InvertX(Physics.gravity gravityState)
    {
        if (gravityState == Physics.gravity.REVERSE)
            isXInverted = true;
        else
            isXInverted = false;
    }

    private void OnDisable()
    {
        OnJumpPressed.RemoveAllListeners();
        OnJumpReleased.RemoveAllListeners();
        OnAttackPressed.RemoveAllListeners();
        OnAttackReleased.RemoveAllListeners();
        OnWeaponChangeButton.RemoveAllListeners();
        OnActivateButton.RemoveAllListeners();
        OnPauseButton.RemoveAllListeners();

        OnUnpauseButton.RemoveAllListeners();

        OnConfirmPressed.RemoveAllListeners();

        OnDeadKeyPress.RemoveAllListeners();

    } 
}
