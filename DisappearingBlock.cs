using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{
    public float disappearTime;
    bool active = false;
    int numCollisions = 0;  // number of objects colliding with blcok
    private float elapsedTime = 0;
    private float triggerTime = .1f;

    Animator animator;
    BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active == false)
        {
            // timer prevents activating by hitting from below
            elapsedTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && active == false)
            elapsedTime = 0;

    }

    private void Update()
    {
        if (elapsedTime > triggerTime && active == false)
        {
            elapsedTime = 0;
            StartCoroutine(Activate());
        }
    }

    // Keep track of collisions. Block will not reappear if colliding
    // with another object.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        numCollisions += 1;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        numCollisions -= 1;
    }


    IEnumerator Activate()
    {
        active = true;
        //Frame 1
        animator.SetInteger("Frame", 1);
        yield return new WaitForSeconds(disappearTime*.5f);

        //Frame 2
        animator.SetInteger("Frame", 2);
        yield return new WaitForSeconds(disappearTime*.5f);


        //Frame 4
        animator.SetInteger("Frame", 4);
        // Move to layer player does not interact with
        gameObject.layer = LayerMask.NameToLayer("Disabled");
        yield return new WaitForSeconds(4);

        //Reappear
        // Don't reappear if object is colliding with block
        while (numCollisions > 0)
            yield return new WaitForSeconds(.2f);

        animator.SetInteger("Frame", 0);
        gameObject.layer = LayerMask.NameToLayer("Ground"); ;
        active = false;
        elapsedTime = 0;
        
    }

}
