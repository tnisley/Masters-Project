using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{

    // Left/right movement
    public float MaxSpeed = 7f;
    public float MovementSpeed { get; private set; } = 7f;

    // Jumping
    public float JumpVelocity { get; private set; } = 16f;
    public float earlyJumpAllowed = .06f; // allow input to be read a little early for responsiveness
    public float lateJumpAllowed = .03f; // allow input to be read a little late responsiveness

    // Climbing
    public float climbSpeed = 3f;
    public float jumpOffVelocity = 10f;

    //use to filter collisions to just enemies
    //protected ContactFilter2D enemyFilter;
    //protected LayerMask enemyMask;

    public GameObject gameMasterObject;    //Ref to GameMaster object
    private GameMaster gameMasterScript;      // Ref to GameMAster Script
    private GameObject interactiveObject;   // holds switch/door player is currently interacting with

    public bool isInLadderZone = false;
    public Collider2D currentLadder;
    public bool isOnPlatform = false;

    public GameObject enemyCollision;
    public bool canBeDamaged = true;
    public float invulnerabilityDuration;
    public Vector2 knockback;


    public bool FacingRight { get; protected set; } = true;
    public bool UpsideDown { get; protected set; } = false;

    // Events
    public AnimationEvents animEvents;

    #region Components
    public Animator animator;
    public InputHandler input;
    public GameObject weaponObject;
    public Physics physics;
    public Rigidbody2D rbody;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public Health health;


    #endregion

    // Allow attack and move to be disabled
    public bool attackDisabled;
    public bool moveDisabled;
    public bool jumpDisabled = false;
    public bool isAttacking = false;


    #region Player State Variables
    // state machine for action states
    StateMachine actionStateMachine;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerFallState fallState;
    public PlayerClimbState climbState;
    public PlayerKnockbackState damageState;
    public PlayerDeadState deadState;

    // state machine for damage states
    StateMachine damageStateMachine;
    public PlayerNormalDamageState normalDamageState;
    public PlayerTempInvulnerableState tempInvulnerableState;

    // Contains player moves
    public PlayerMovement movement;

    #endregion

    private void Awake()
    {
        movement = new PlayerMovement(this);
        animEvents = new AnimationEvents();

        // Initialize states/state machine
        actionStateMachine = new StateMachine();

        idleState = new PlayerIdleState(this, actionStateMachine, "isIdle");
        moveState = new PlayerMoveState(this, actionStateMachine, "isWalking");
        jumpState = new PlayerJumpState(this, actionStateMachine, "isJumping");
        fallState = new PlayerFallState(this, actionStateMachine, "isFalling");
        climbState = new PlayerClimbState(this, actionStateMachine, "climbLadder");
        damageState = new PlayerKnockbackState(this, actionStateMachine, "damage");
        deadState = new PlayerDeadState(this, actionStateMachine, "isDead");

        actionStateMachine.AddState(idleState);
        actionStateMachine.AddState(moveState);
        actionStateMachine.AddState(jumpState);
        actionStateMachine.AddState(fallState);
        actionStateMachine.AddState(climbState);
        actionStateMachine.AddState(damageState);
        actionStateMachine.AddState(deadState);

        damageStateMachine = new StateMachine();
        normalDamageState = new PlayerNormalDamageState(this, damageStateMachine);
        tempInvulnerableState = new PlayerTempInvulnerableState(this, damageStateMachine);

        damageStateMachine.AddState(normalDamageState);
        damageStateMachine.AddState(tempInvulnerableState);

        // cache components
        input = GetComponent<InputHandler>();
        physics = GetComponent<Physics>();
        rbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        health = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    protected void Start()
    {
        actionStateMachine.Initialize(idleState);
        damageStateMachine.Initialize(normalDamageState);
        interactiveObject = null;

        // Register for events
        GameEvents.OnPlayerEnterLadderZone.AddListener(EnterLadderZone);
        GameEvents.OnPlayerExitLadderZone.AddListener(ExitLadderZone);
        InputHandler.OnActivateButton.AddListener(ActivateCurrentObject);
    }

    protected void FixedUpdate()
    {
        Debug.Log("Current state: " + actionStateMachine.CurrentState.ToString());
            actionStateMachine.CurrentState.StateFixedUpdate();
            damageStateMachine.CurrentState.StateFixedUpdate();
    }

    void Update()
    {
            SetPlayerDirection();
            damageStateMachine.CurrentState.StateUpdate();
            actionStateMachine.CurrentState.StateUpdate();
    }

    public void TakeDamage(DamageData damage)
    {
        if (canBeDamaged)
        {
            enemyCollision = damage.gameObject;

            health.Damage(damage.dmgAmount);
            GameEvents.OnPlayerHealthChange.Invoke(health.GetCurrentHealth());

            if (health.IsDead())
            {
                GameEvents.OnPlayerDeath.Invoke();
                Debug.Log("Player has died");
            }
            else
            {
                GameEvents.OnPlayerDamage.Invoke();
            }
        }
    }

    #region Player Collisions

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Keep track of interactive object player can activate
        if (collision.collider.CompareTag("InteractiveObject"))
            interactiveObject = collision.gameObject;

        else if (collision.collider.CompareTag("Collectible"))
        {
            Debug.Log("Collide with item");
            CollectItem(collision.collider.gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hazards") && canBeDamaged)
        {
            Debug.Log("Hazard collision");
            // Hazards only set to do 1 hp of damage for now.
            TakeDamage(new DamageData(collision.collider.gameObject, 1));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // no longer can activate an interactive object
        if (collision.collider.CompareTag("InteractiveObject"))
            interactiveObject = null;
    }

    // Handle ground collisions
    public void OnGroundCollisionStay(Collider2D other)
    {
        if (other.CompareTag("Conveyor"))
        {
            Vector2 velocityToAdd = other.GetComponent<IMover>().GetVelocity(); ;
            physics.AddExternalForce(velocityToAdd);
        }
        if (other.CompareTag("MovingPlatform"))
        {
            Vector2 velocityToAdd = other.GetComponent<IMover>().GetVelocity(); ;
            velocityToAdd *= Mathf.Sign(physics.gravityScale);

            physics.AddExternalForce(velocityToAdd);

        }
    }

    public void OnGroundCollisionEnter(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            isOnPlatform = true;
            Debug.Log("Entered Moving Platform");
            //gameObject.transform.SetParent(other.transform);

        }
    }

    public void OnGroundCollisionExit(Collider2D other)
    {
        Debug.Log("On Collsiion Exit called");
        if (other.CompareTag("MovingPlatform"))
        {
            isOnPlatform = false;
            gameObject.transform.parent = null;
            Debug.Log("Exited Moving Platform");
        }
    }

    #endregion

    // Flips the player sprite if they change directions
    void SetPlayerDirection()
    {
        // Reverse Gravity
        if (physics.GravityState == Physics.gravity.REVERSE)
        {
            // Vertical orientation
            if (!UpsideDown)
            {
                this.transform.Rotate(new Vector3(180, 0, 0), Space.Self);
                UpsideDown = true;
            }

            // face left
            if (input.XInput > 0f && FacingRight)
            {
                this.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                FacingRight = false;
            }
            // face right
            else if (input.XInput < 0f && !FacingRight)
            {
                this.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                FacingRight = true;
            }
        }

        // Normal Gravity
        else
        {
            // Vertical orientation
            if (UpsideDown)
            {
                this.transform.Rotate(new Vector3(180, 0, 0), Space.Self);
                UpsideDown = false;
            }

            // face left
            if (input.XInput < 0f && FacingRight)
            {
                this.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                FacingRight = false;
            }
            // face right
            else if (input.XInput > 0f && !FacingRight)
            {
                this.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                FacingRight = true;
            }
        }
    }

    private void EnterLadderZone(Collider2D ladder)
    {
        isInLadderZone = true;
        currentLadder = ladder;
    }

    private void ExitLadderZone()
    {
        isInLadderZone = false;
        currentLadder = null;
    }

    // Activate switches and other objects
    private void ActivateCurrentObject()
    {
        if (interactiveObject != null )
            interactiveObject.GetComponent<iInteractive>().Activate();
    }

    // pick up a collectible
    private void CollectItem(GameObject item)
    {
        CollectibleData itemData = item.GetComponent<Collectible>().data;

        GameEvents.OnGetCollectible.Invoke(itemData);

        // If health item, heal the player
        if (itemData.itemType == CollectibleData.Type.HEALTH)
        {
            health.Heal(itemData.value);
            GameEvents.OnPlayerHealthChange.Invoke(health.GetCurrentHealth());
        }

       Destroy(item);
    }

    // Called from animation to signal the animation is finished.
    public void AnimationFinished()
    {
        animEvents.OnAnimationFinished.Invoke();
    }

    // Prevents enemy from updating when dead
    public void SetDead()
    {
        physics.enabled = false;
        boxCollider.enabled = false;
    }

    private void OnDisable()
    {
        damageStateMachine.Destroy();
        actionStateMachine.Destroy();

        GameEvents.OnPlayerEnterLadderZone.RemoveListener(EnterLadderZone);
        GameEvents.OnPlayerExitLadderZone.RemoveListener(ExitLadderZone);
        InputHandler.OnActivateButton.RemoveListener(ActivateCurrentObject);
    }
}
