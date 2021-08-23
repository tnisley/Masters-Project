using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool canDestroy = true;
    protected BoxCollider2D enemyCollider;
    protected Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Play damage sequence. Called from player attack.
    public void TakeDamage(float damageTaken)
    {
        if (canDestroy)
        {

            // disable collider so player can't get hurt during death animation
            enemyCollider.enabled = false;
            animator.SetBool("isDead", true);
        }
    }

    // Destroy function to be called from animator
    public void DestoryEnemy()
    {
        Destroy(gameObject);
    }
}
