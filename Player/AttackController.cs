using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject weaponObject;
    public IWeapon currentWeapon;
    public bool isAttacking = false;
    Player player;
    Animator animator;

    public WeaponManager weaponManager;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        weaponManager = GetComponent<WeaponManager>();
        InputHandler.OnAttackPressed.AddListener(HandleAttackPress);
    }


    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weaponManager.CurrentWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = weaponManager.CurrentWeapon;
    }

    private void FixedUpdate()
    {
    }

    void HandleAttackPress()
    {
        if (currentWeapon.CanAttack() && !isAttacking)
        {
            BeginAttack();
            currentWeapon.UseWeapon();
        }
    }

    // Handle player attack begin and end. Set player attack status and 
    // invoke attack events. 
    // EndAttack should be called from the attack animation.
    private void BeginAttack()
    {
        player.isAttacking = true;
        GameEvents.OnPlayerAttackBegin.Invoke();
    }

    private void EndAttack()
    {
        player.isAttacking = false;
        GameEvents.OnPlayerAttackEnd.Invoke();
    }

    private void OnDestroy()
    {
        InputHandler.OnAttackPressed.RemoveListener(HandleAttackPress);

    }
}
