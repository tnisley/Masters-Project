using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public bool isActive;
    public EnemyData data;
    public GameObject player;
    protected LayerMask whatIsGround;
    protected LayerMask whatIsWall;

    protected Vector2 velocity;
    public EnemyProjectileWeapon projectileWeapon;

    public EnemyEvents events;
    public AnimationEvents animEvents;

    // Positions for raycasts
    [SerializeField]
    Transform wallCheck;
    [SerializeField]
    Transform edgeCheck;
    [SerializeField]
    Transform playerCheck;
    [SerializeField]
    Transform rearWallCheck;
    [SerializeField]
    Transform rearEdgeCheck;

    #region Components

    [HideInInspector] public Rigidbody2D rbody;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Health health;
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public BoxCollider2D boxCollider;


    #endregion

    protected virtual void Awake()
    {
        // cache components
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        
        if (data.canBeDamaged)
            health = GetComponent<Health>();

        events = new EnemyEvents();
        animEvents = new AnimationEvents();

        SetGround();
        whatIsWall = LayerMask.GetMask("Ground");
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (!data.startWhenInView)
            Activate();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Collision wiht player - apply contact damage
        if (collision.collider.CompareTag("Player") && data.contactDamage > 0)
        {
            GameEvents.SendDamageMessage(collision.collider.gameObject, new DamageData(gameObject, data.contactDamage));
            Debug.Log("Enemy-Player collision");
        }
    }

    // Execute when enemy becomes active
    protected virtual void Activate()
    {
        isActive = true;
    }

    public virtual void TakeDamage(DamageData damage)
    {
        if (data.canBeDamaged && health != null)
        {
            Debug.Log(damage.dmgAmount);
            health.Damage(damage.dmgAmount);

            if (health.IsDead())
            {
                events.OnEnemyDeath.Invoke();
                Debug.Log("ENEMY KILLED!");
            }
            else
                events.OnEnemyDamage.Invoke();
        }
    }

    // Activate enemy after it is in view
    protected void OnBecameVisible()
    {
            Activate();
    }

    // Set ground layer mask
    protected virtual void SetGround()
    {
        if (data.useGround && data.usePlatforms)
            whatIsGround = LayerMask.GetMask("Ground", "Platforms");

        else if (data.useGround)
            whatIsGround = LayerMask.GetMask("Ground");

        else if (data.usePlatforms)
            whatIsGround = LayerMask.GetMask("Platforms");
    }

    // Change the enemy's direction
    public virtual void FlipSprite()
    {
        this.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }


    // Detect if a wall is in front of enemy
    public virtual bool IsAtWall()
    {
        return (Physics2D.Raycast(wallCheck.position, -transform.right, data.wallDistance, whatIsWall));
    }

    // Detect if enemy is at the edge of a platform
    public virtual bool IsAtEdge()
    {
        return !(Physics2D.Raycast(edgeCheck.position, -transform.up, data.edgeDistance, whatIsGround));
    }

    public virtual bool isAtWallBehind()
    {
        return (Physics2D.Raycast(rearWallCheck.position, transform.right, data.wallDistance, whatIsWall));
    }

    public virtual bool isAtEdgeBehind()
    {
        return !(Physics2D.Raycast(rearEdgeCheck.position, -transform.up, data.edgeDistance, whatIsGround));
    }

    public virtual bool isTargetInAttackRange()
    {
        return Physics2D.Raycast(playerCheck.position, -transform.right, data.attackDistance, data.whatIsTarget);
    }

    public virtual bool isTargetInMinCloseRange()
    {
        return Physics2D.Raycast(playerCheck.position, -transform.right, data.minCloseRangeDistance, data.whatIsTarget);

    }
    public virtual bool isTargetInMaxCloseRange()
    {
        return Physics2D.Raycast(playerCheck.position, -transform.right, data.maxCloseRangeDistance, data.whatIsTarget);

    }

    // Functions to be called from other classes to set velocity.
    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    public void SetVelocityX(float velocity)
    {
        this.velocity.x = velocity;
    }

    public void SetVelocityY(float velocity)
    {
        this.velocity.y = velocity;
    }

    // Called from animation to signal the animation is finished.
    public virtual void AnimationFinished()
    {
        animEvents.OnAnimationFinished.Invoke();
    }

    // Kill the enemy
    public virtual void Die()
    {
        boxCollider.enabled = false;
    }

    // Visualize various components
    protected virtual void OnDrawGizmos()
    {
        if (wallCheck && data)
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + data.wallDistance * -transform.right);
        if (edgeCheck && data)
            Gizmos.DrawLine(edgeCheck.position, edgeCheck.position + data.edgeDistance * -transform.up);
        if (rearWallCheck && data)
            Gizmos.DrawLine(rearWallCheck.position, rearWallCheck.position + data.wallDistance * transform.right);
        if (rearEdgeCheck && data)
            Gizmos.DrawLine(rearEdgeCheck.position, rearEdgeCheck.position + data.rearEdgeDistance * -transform.up);
        if (playerCheck && data)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + data.attackDistance * -transform.right);
        }
    }
}
