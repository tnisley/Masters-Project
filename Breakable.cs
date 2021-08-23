using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    protected BoxCollider2D objectCollider;
    protected Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Play breaking animation.
    public void BreakObject()
    {
            animator.SetBool("break", true);
    }

    // Destroy function to be called from animator
    public void DestoryObject()
    {
        Destroy(gameObject);
    }
}
