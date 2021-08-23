using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract weapon class to be inherited by all subclasses.
// All weapons should be saved as prefabs.

public abstract class Weapon : MonoBehaviour, IWeapon
{

    public WeaponData data;
    [SerializeField]
    protected int id;
    protected float lastAttackTime;

    public string animName;
    protected Animator animator;
    
    // Set animator and initialize object as not active
    public virtual void OnEnable()
    {
        Debug.Log("Getting Animator in weapon");
        animator = gameObject.GetComponentInParent<Animator>();
        Debug.Log("Setting last attack time");

        lastAttackTime = data.attackDelay * -1;
    }

    public int GetId()
    {
        return id;
    }

    // To be implemented in subclasses
    protected virtual void Update()
    { }

    public virtual void PlayAttackAnimation()
    {
        animator.Play(animName);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public abstract bool CanAttack();


    public abstract void UseWeapon();
    
}
