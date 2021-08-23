using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple movement controller. Does not have gravity, ground checks or
// collision resolution and does not handle slopes. Useful for simple enemies that will already 
// check for collisions in their movement and do not the physics overhead.

public class SimpleMoveController : MonoBehaviour
{
    Rigidbody2D rbody;

    private void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        rbody.MovePosition((Vector2)transform.position + velocity * Time.deltaTime);
    }




}
