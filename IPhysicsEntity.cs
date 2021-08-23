using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// allows a game object to interact with the custom physics system.

public interface IPhysicsEntity
{
    // Replaces Unity's OnCollisionEnter for ground collisions.
    // Unity's OnCollisionEnter will not work with ground collisions.
    void OnGroundCollision(Collider2D collider);


}
