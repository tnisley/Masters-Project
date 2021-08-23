using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO - Create base EnemyWeapon class that implements IWeapon.
// This class should then inherit from EnemyWeapon.

public class EnemyProjectileWeapon : MonoBehaviour
{
    public int maxProjectiles = 16;
    public Vector2 projectileVelocity;
    public Projectile projectilePrefab;

    protected ProjectilePool projectiles;
    protected Projectile currentProjectile; // reference to next active projectile

    public bool CanAttack()
    {
        return projectiles.HasNext();
    }

    public void OnEnable()
    {
        projectiles = new ProjectilePool(maxProjectiles, projectilePrefab);
    }

    public void UseWeapon()
    {
        currentProjectile = projectiles.Get();
        if (currentProjectile != null) // check for safety
        {
            currentProjectile.GetComponent<IProjectile>().Launch(transform.position, projectileVelocity * -gameObject.transform.right.x);
        }
    }
}
