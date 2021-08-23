/* Custom Physics for 2D Platformer
 * Handles movement, gravity and collision with "Ground" layer
 * All floors/walls/ceilings/platforms should be placed on the "Ground" Layer
 * All other collisions should be handled in another class
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]

public class Physics : MonoBehaviour
{

    public bool gravityEnabled;
    public bool isAffectedByGravityChange;

    #region Gravity Constants
    // Used to set gravity state
    public enum gravity { NORMAL, LOW, REVERSE };
    public gravity GravityState { get; private set; } = gravity.NORMAL;

    // Gravity values
    public const float gravityConstant = -35f;
    private const float normalGravity = 1f;
    private const float lowGravity = .6f;
    private const float reverseGravity = -1f;
    #endregion

    // Physics values
    public float gravityScale { get; private set; } = 0;  // holds scale of gravity
    private const float minGroundNormalY = 0.65f;  // max angle of slope that is considered ground
    protected const float terminalVelocity = 15f;  // terminal velocity of falling object (positive value

    // Flags
    protected bool isGrounded;        // is object on ground
    protected bool setVelocityFlag = false;  // directly set object's position

    [HideInInspector] public GameObject currentGround { get; private set; } // Current ground object
    protected Vector2 groundNormal; // Normal vector of the ground surface
    protected Vector2 deltaPosition;  // change in object's position each frame
    protected Vector2 velocity;  // object's current velocity
    protected Vector2 targetVelocity;   // stores physics acceleration force
    protected Vector2 externalForce; // external force added to player by another object
    public Vector2 PreviousVelocity { get; protected set; } // store velocity of last frame

    #region Components
    protected Rigidbody2D rbody;
    protected Collider2D objectCollider;
    protected Transform objectTransform;
    #endregion

    #region Collision Detection

    protected ContactFilter2D groundFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    // Minimum distance to move before checking for collisions
    const float minMoveDistance = 0.001f;
    // padding for the collider (prevents getting stuck in wall)
    const float skinWidth = 0.025f;

    // Used for calling OnGroundCollision Functions
    protected List<Collider2D> previousColliders = new List<Collider2D>();
    protected List<Collider2D> currentColliders = new List<Collider2D>();

    #endregion

    [SerializeField] private PhysicsMaterial defaultMaterial;
    [SerializeField] private PhysicsMaterial airMaterial;
    public PhysicsMaterial CurrentMaterial { get; private set; }

    protected virtual void OnEnable()
    {
        rbody = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<Collider2D>();
        groundFilter.useLayerMask = true;

        if (gravityEnabled)
        {
            //SetInitialGravity();
            SetGravity(GravityState);
        }

        if (isAffectedByGravityChange)
        {
            GameEvents.OnGravityChange.AddListener(SetGravity);
            GameEvents.OnFlipGravity.AddListener(FlipGravity);
        }
    }

    protected virtual void Start()
    {
        
    }

    // Update for physics
    protected virtual void FixedUpdate()
    {
        // Move using player velocity
        ////Compute velocity if not setting velocity directly
        if (!setVelocityFlag)
        {
            ComputeVelocity();
            PreviousVelocity = velocity;
        }

        // Print Velocity
        Debug.Log(gameObject.ToString() + "'s velocity: " + velocity);

        deltaPosition = ComputeDeltaP(velocity + externalForce);
        externalForce = Vector2.zero;

        Move(deltaPosition);
        CurrentMaterial = GetGroundMaterial();

        // save velocity if using physics to move
        if (setVelocityFlag)
            PreviousVelocity = Vector2.zero;
        else
            PreviousVelocity = velocity;

        // Reset for next update
        setVelocityFlag = false;
        targetVelocity = Vector2.zero;

    }

    // Compute the x and y velocities.
    // Gets values from calls to SetAcceleration() and also
    // adds gravity. 
    protected void ComputeVelocity()
    {
        // Add velocity to y
        velocity.y = targetVelocity.y;

        // Add gravity
        if (gravityEnabled)
            velocity.y += PreviousVelocity.y + gravityScale * gravityConstant * Time.deltaTime;

        // limit fall velocity to terminal velocity
        if (GravityState != gravity.REVERSE)
            velocity.y = Mathf.Max(velocity.y, -terminalVelocity * gravityScale);

        // Reverse Gravity
        else if (GravityState == gravity.REVERSE)
            velocity.y = Mathf.Min(velocity.y, terminalVelocity);

        //set x velocity
        velocity.x = targetVelocity.x;

    }

    // Compute the change in object's position. Adds
    // external forces.
    private Vector2 ComputeDeltaP(Vector2 velocity)
    {
        // Get change in object's position (x and y)
        Vector2 deltaPosition = velocity * Time.deltaTime;

        return deltaPosition;
    }



    protected void Move(Vector2 deltaPosition)
    {
        // Get angle of ground to move along
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        // Get horizontal movement value
        Vector2 tempMove = moveAlongGround * deltaPosition.x;

        // Resolve horizontal collisions
        Vector2 move = ResolveCollisions(tempMove, false);

        // Get the vertical movement value
        tempMove = Vector2.up * deltaPosition.y;
        // Resolve vertical collisions
        move += ResolveCollisions(tempMove, true);

        // Move the object
        rbody.position = rbody.position + move;
    }

    // Adjust move based on collision with ground. 
    // Also check if grounded.
    private Vector2 ResolveCollisions(Vector2 move, bool verticalMove)
    {
        // set what is ground
        if (verticalMove)
            groundFilter.layerMask = LayerMask.GetMask("Ground", "Platforms");

        else
            groundFilter.layerMask = LayerMask.GetMask("Ground");


        float distance = move.magnitude;

        // Check for collisions (if move is above a minimum threshold)
        if (distance > minMoveDistance)
        {
            // Cast rigidbody to the new move position to potential detect collisions
            int collisionCount = rbody.Cast(move, groundFilter, hitBuffer, distance + skinWidth);

            isGrounded = false;

            // Handle collision with ground layer
            for (int i = 0; i < collisionCount; i++)
            {

                // only collide with one-way platform if player is above it
                if (verticalMove && hitBuffer[i].collider.gameObject.layer == LayerMask.NameToLayer("Platforms"))
                {
                    if (gravityScale >= 0)
                    {
                        if (!objectCollider.IsAbove(hitBuffer[i].collider))
                            continue;
                    }

                    else if (gravityScale < 0)
                    {
                        if (!objectCollider.IsBelow(hitBuffer[i].collider))
                            continue;

                    }
                }


                Vector2 currentNormal = hitBuffer[i].normal;

                // Is object on the ground
                if (GravityState != gravity.REVERSE && currentNormal.y > minGroundNormalY ||
                    GravityState == gravity.REVERSE && currentNormal.y < 1 - minGroundNormalY)
                {
                    // Sets ground to closest tile hit
                    if (!isGrounded)    // first ground tile hit
                        currentGround = hitBuffer[i].collider.gameObject;

                    // Will be grounded on next frame
                    isGrounded = true;
                    Debug.Log("Grounded");

                    // set new ground normal
                    if (verticalMove)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }

                    // Call OnGroundCollision in the game object
                    // Similar to Unity's OnCollision functions
                    CallOnCollision(hitBuffer[i].collider);
                    
                }
                else
                    currentGround = null;

                // Adjusts velocity on if inside a collider
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                // If move collides with ground layer, set move to distance from ground tile
                float hitDistance = hitBuffer[i].distance - skinWidth;
                distance = Mathf.Min(distance, hitDistance);

                // if we hit ceiling, reset y velocity to 0
                // this is needed when player collides with a ceiling corner
                if (verticalMove && gravityScale * move.y > 0f)
                    velocity.y = 0f;
            }

            if (!isGrounded)
                currentGround = null;
        }

        if (verticalMove)
        {
            // call OnGroundCollisionExit on remaining colliders
            if (previousColliders != null)
            {
                foreach (Collider2D collider in previousColliders)
                {
                    gameObject.SendMessage("OnGroundCollisionExit", collider, SendMessageOptions.DontRequireReceiver);
                }
                previousColliders.Clear();
            }

            // reset collider lists
            foreach (Collider2D collider in currentColliders)
                previousColliders.Add(collider);
            currentColliders.Clear();
        }

        return move.normalized * distance;
    }

    // Set gravity and initial ground normal values when
    // gravity change event is invoked.
    // Without setting initial ground normal, player will move wrong
    // direction when gravity is changed. Correct direction not achieved
    // until player hits ground and ground normal is updated.
    private void SetGravity(gravity newState)
    {
        GravityState = newState;
        switch (GravityState)
        {
            case gravity.NORMAL:
                gravityScale = normalGravity;
                groundNormal.y = 1.0f;
                break;

            case gravity.LOW:
                gravityScale = lowGravity;
                groundNormal.y = 1.0f;
                break;

            case gravity.REVERSE:
                gravityScale = reverseGravity;
                groundNormal.y = -1.0f;
                break;

            default:
                gravityScale = normalGravity;
                break;
        }
    }

    private void FlipGravity()
    {
        if (GravityState == gravity.NORMAL)
            GameEvents.OnGravityChange.Invoke(gravity.REVERSE);
        else if (GravityState == gravity.REVERSE)
            GameEvents.OnGravityChange.Invoke(gravity.NORMAL);

    }

    // Directly set velocity for non-physics movement.
    // Will override any forces applied to object.
    public void SetVelocity(Vector2 velocity)
    {
        setVelocityFlag = true;
        this.velocity = velocity;
    }

    // Add acceleration to an object.
    public void AddVelocity(Vector2 velocity)
    {
        targetVelocity += velocity;
    }

    // Add non-physics force to an object
    public void AddExternalForce(Vector2 velocity)
    {
        externalForce += velocity;
    }



    // Return whetrer the object is on the ground
    public bool IsGrounded()
    {
        return isGrounded;
    }

    // Get current Velocity
    public Vector2 GetVelocity()
    {
        return velocity;
    }

    // Get Velocity from previous frame
    public Vector2 GetPrevVelocity()
    {
        return PreviousVelocity;
    }

    // Resets velocity to zero
    // Used to cancel momentum from physics based movement.
    public void ResetVelocity()
    {
        velocity = Vector2.zero;
        PreviousVelocity = Vector2.zero;
    }

    protected PhysicsMaterial GetGroundMaterial()
    {
        if (currentGround == null)
            return airMaterial;
        else
        {
            ObjectMaterial groundMaterial = currentGround.GetComponent<ObjectMaterial>();
            if (groundMaterial == null)
                return defaultMaterial;
            else
                return groundMaterial.Get();
        }
    }

    // Calls the appropriate OnGroundCollision function for the collider
    protected void CallOnCollision(Collider2D collider)
    {
        currentColliders.Add(collider);

        if (previousColliders.Contains(collider))
        {
            gameObject.SendMessage("OnGroundCollisionStay", collider, SendMessageOptions.DontRequireReceiver);
            previousColliders.Remove(collider);
        }

        else
        {
            gameObject.SendMessage("OnGroundCollisionEnter", collider, SendMessageOptions.DontRequireReceiver);
            gameObject.SendMessage("OnGroundCollisionStay", collider, SendMessageOptions.DontRequireReceiver);
        }

    }

    private void OnDestroy()
    {
        GameEvents.OnGravityChange.RemoveListener(SetGravity);
        GameEvents.OnFlipGravity.RemoveListener(FlipGravity);
    }
}
