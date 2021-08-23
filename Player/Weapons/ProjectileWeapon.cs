using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Projectile weapon uses a pool of projectiles to fire.

public class ProjectileWeapon : Weapon
{
    //[SerializeField]
    public ProjectilePool projectiles;
    public Projectile projectilePrefab; // projectile reference
    public Projectile currentProjectile; // reference to next active projectile

    public int maxProjectiles = 1; // max projectiles on screen at once
    public Vector2 projectileVelocity;
    [SerializeField]
    protected Vector2 projectileOffset;


    public override void OnEnable()
    {
        base.OnEnable();
        projectiles = new ProjectilePool(maxProjectiles, projectilePrefab);
    }
    
    

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanAttack()
    {
        Debug.Log(Time.time + " - " + lastAttackTime);
        return (Time.time - lastAttackTime > data.attackDelay && projectiles.HasNext());
    }

    public override void UseWeapon()
    {
        currentProjectile = projectiles.Get();
        if (currentProjectile != null) // check for safety
        {
            lastAttackTime = Time.time;
            PlayAttackAnimation();

            Vector2 origin = new Vector2(transform.position.x + projectileOffset.x * gameObject.transform.right.x, transform.position.y + projectileOffset.y);
            currentProjectile.GetComponent<IProjectile>().Launch(origin, projectileVelocity * gameObject.transform.right.x);
        }
    }

    // Draw marker when selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, .125f);
    }
}
