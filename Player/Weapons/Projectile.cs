using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Template for projectile objects
// Default projectile moves in straight line,
// damages enemies, and stops when hitting wall or enemy
// All projectiles should be saved as prefabs.

[RequireComponent(typeof(BoxCollider2D), typeof (Rigidbody2D))]
public class Projectile : MonoBehaviour, IProjectile
{
    public ProjectileData data;

    // Allows setting of target in projectile data
    public enum Target { NONE, PLAYER, ENEMY };

    private Collider2D collider;
    public Rigidbody2D rbody;

    // movement data
    protected Vector2 targetVelocity;
    protected Vector2 move;
    protected Vector2 origin;



    // Only onEnable is called on dynamically instantiated objects
    protected virtual void OnEnable()
    {
        collider = GetComponent<Collider2D>();
        rbody = GetComponent<Rigidbody2D>();
    }


    protected virtual void FixedUpdate()
    {
        Move();

        // check bounds
        if (ExceedsMaxDistance())
            OnMaxDistance();
        else if (ExceedsCameraBounds())
            OnExceedsCameraBounds();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision);
    }

    public virtual void Launch(Vector2 position, Vector2 velocity)
    {
        gameObject.SetActive(true);
        origin = position;
        targetVelocity = velocity;

        // Place projectile in starting position
        transform.position = origin;
        rbody.position = origin;

        LaunchAction();
    }

    protected bool ExceedsMaxDistance()
    {
        if (data.maxDistance > 0.01f)
        {
            return Vector2.Distance(origin, rbody.position) > data.maxDistance;
        }
        else  // no max distance
            return false;
    }

    protected bool ExceedsCameraBounds()
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(rbody.position);
        if (viewportPos.x > 1 || viewportPos.x < 0 ||
            viewportPos.y > 1 || viewportPos.y < 0)
            return true;
        else
            return false;
    }

    // The following can be overridden by subclasses:
    protected virtual void Move()
    {
        move = targetVelocity * Time.deltaTime;
        rbody.MovePosition(rbody.position + move);
    }

    protected virtual void OnMaxDistance()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnExceedsCameraBounds()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollision(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && data.target == Target.ENEMY)
        {
            OnTargetCollision(collision.gameObject);
        }

        else if (collision.collider.CompareTag("Player") && data.target == Target.PLAYER)
        {
            OnTargetCollision(collision.gameObject);
        }

        else if (collision.collider.CompareTag("Ground"))
        {
            OnGroundWallCollision(collision.gameObject);
        }

        // Adjust to make projectile destroy breakable blocks
        else if (collision.collider.CompareTag("Breakable"))
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnGroundWallCollision(GameObject target)
    {
        gameObject.SetActive(false);
    }


    protected virtual void OnTargetCollision(GameObject target)
    {
        GameEvents.SendDamageMessage(target, new DamageData(gameObject, data.damageAmount));
        gameObject.SetActive(false);
    }

    // animate, etc.
    protected virtual void LaunchAction()
    {}


    //public void Disable()
    //{
    //    gameObject.SetActive(false);
    //    OnDisable.Invoke();
    //}
}
