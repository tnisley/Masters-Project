using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// contains data and routines for a basic damage handler.

[RequireComponent(typeof (SpriteRenderer))]
public class Damage : MonoBehaviour
{
    bool invulnerable = false;
    public float invulnerabilityDuration;

    protected SpriteRenderer sprite;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Makes the game object invulnerable.
    public void SetInvulnerable()
    {
        StartCoroutine(SetInvlunerableCR());
    }

    // coroutine called by SetInvulnerable
    IEnumerator SetInvlunerableCR()
    {
        Color origColor = sprite.color;
        sprite.SetAlpha(.5f);
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);

        sprite.color = origColor;
        yield return new WaitForSeconds(.5f); // leeway for the player
        invulnerable = false;
    }

    public bool isInvulnerable()
    {
        return invulnerable;
    }
}
