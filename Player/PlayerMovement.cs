using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class contains the player's moves.
 * Gets material from physics and applies
 * the proper forces to movement. Each move sets the corresponding 
 * velocity in the physics class.
*/

public class PlayerMovement
{
    Player player;
    Vector2 prevVelocity;

    // Constructor
    public PlayerMovement(Player player)
    {
        this.player = player;
    }

    // Adds friction if player is not adding x input
    public void ApplyFriction()
    {
        prevVelocity = player.physics.PreviousVelocity;

        if (prevVelocity.x != 0f)
        {
            float friction = player.MaxSpeed * player.physics.CurrentMaterial.friction;
            if (prevVelocity.x < 0f)
                friction *= -1;

            float newVelocity = prevVelocity.x - friction;

            // Stop decelerating when we reach 0
            if (Mathf.Sign(prevVelocity.x) != Mathf.Sign(newVelocity))
                newVelocity = 0f;

            // set player x velocity
            player.physics.AddVelocity(new Vector2(newVelocity, 0f));
        }
    }

    // Adds horizontal velocity based on current direction
    // and player input.
    public void MoveHorizontal()
    {
        prevVelocity = player.physics.PreviousVelocity;
        int input = player.input.XInput;
        PhysicsMaterial material = player.physics.CurrentMaterial;

        // Input from standstill or in current moving direction
        if ( (prevVelocity.x >= 0f && player.input.XInput >= 0f) ||
             (prevVelocity.x <= 0f && player.input.XInput <= 0f) )
            Accelerate(material);

        // Input opposite of current moving direction
        else
            Decelerate(material);
    }

    // Add vertical velocity for jump
    public void Jump()
    {
        float jumpVelocity = player.JumpVelocity * player.physics.CurrentMaterial.jumpMultiplier;
        if (player.physics.GravityState == Physics.gravity.REVERSE)
            jumpVelocity *= -1;

        player.physics.AddVelocity(new Vector2(0f, jumpVelocity));
    }

    // used to set a specific jump height, like when jumping off a climbable object
    public void Jump(float yVelocity)
    {
        if (player.physics.GravityState == Physics.gravity.REVERSE)
            yVelocity *= -1;
        player.physics.AddVelocity(new Vector2(0f, yVelocity));
    }

    // Decrease Y velocity if jump is released
    public void CancelJump()
    {
        Debug.Log("Cancelling jump");
        player.physics.AddVelocity(new Vector2(0f, -player.physics.GetVelocity().y * .3f));
    }

    // Climb an object
    public void Climb()
    {
        float move = player.climbSpeed * player.input.YInput;
        player.physics.SetVelocity(new Vector2(0f, move));

    }

    // Add velocity to player in direcition of movement
    private void Accelerate(PhysicsMaterial material)
    {
        // Calculate new velocity
        prevVelocity = player.physics.PreviousVelocity;
        float velocityToAdd = player.input.XInput * player.MovementSpeed * material.acceleration;
        float newVelocity = prevVelocity.x + velocityToAdd;

        // Clamp to max speed
        newVelocity = Mathf.Clamp(newVelocity, -player.MaxSpeed * material.speed, player.MaxSpeed * material.speed);

        //Add velocity to player physics
        player.physics.AddVelocity(new Vector2(newVelocity, 0f));
    }

    // Slow player down when they change directions
    private void Decelerate(PhysicsMaterial material)
    {
        float deceleration = Mathf.Sign(prevVelocity.x) * player.MaxSpeed * player.physics.CurrentMaterial.deceleration;

        Debug.Log("Deceleration: " + deceleration);

        float newVelocity = prevVelocity.x - deceleration;

        // set player x velocity
        player.physics.AddVelocity(new Vector2(newVelocity, 0f));
    }

}
