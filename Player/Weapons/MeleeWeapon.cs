using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class MeleeWeapon : Weapon
{
    public MeleeWeaponData meleeData;

    public enum Target { NONE, PLAYER, ENEMY};

    float damageAmount;

    Collider2D attackBox;

    protected void Awake()
    {
        attackBox = GetComponent<Collider2D>();
        DisableHitbox();
        GameEvents.OnPlayerAttackBegin.AddListener(EnableHitbox);
        GameEvents.OnPlayerAttackEnd.AddListener(DisableHitbox);
    }

    private void Start()
    {

    }

    // Attack and return collisions
    public override void UseWeapon()
    {

        lastAttackTime = Time.time;

        PlayAttackAnimation();

    }

    public override bool CanAttack()
    {
        return Time.time - lastAttackTime > data.attackDelay;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // Call the damage function in enemy's health script
        if (collision.collider.CompareTag("Enemy") && meleeData.target == Target.ENEMY)
        {
            GameEvents.SendDamageMessage(collision.gameObject, new DamageData(gameObject, meleeData.damageAmount));
            Debug.Log("Enemy Hit!");
        }
        // Break objects
        else if (collision.collider.CompareTag("Breakable") && meleeData.target == Target.ENEMY)
        {
            collision.collider.GetComponent<Breakable>().BreakObject();
            Debug.Log("Breakable Hit!");

        }
    }


    // Activate attack box when attacking
    private void EnableHitbox()
    {
        attackBox.enabled = true;
    }

    // Deactivate attack box after attack
    private void DisableHitbox()
    {
        attackBox.enabled = false;
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerAttackBegin.RemoveListener(EnableHitbox);
        GameEvents.OnPlayerAttackEnd.RemoveListener(DisableHitbox);
    }

}
