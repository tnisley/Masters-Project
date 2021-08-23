using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    public override void Launch(Vector2 position, Vector2 velocity)
    {
        base.Launch(position, velocity);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void LaunchAction()
    {
        base.LaunchAction();
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void OnCollision(Collision2D collision)
    {
        base.OnCollision(collision);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnExceedsCameraBounds()
    {
        // Do nothing
    }

    protected override void OnGroundWallCollision(GameObject target)
    {
        base.OnGroundWallCollision(target);
    }

    protected override void OnMaxDistance()
    {
        base.OnMaxDistance();
    }

    protected override void OnTargetCollision(GameObject target)
    {
        GameEvents.SendDamageMessage(target, new DamageData(gameObject, data.damageAmount));
    }
}
