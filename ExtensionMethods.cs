using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Conatins various functions that extend Unity's
// built-in classes

public static class ExtensionMethods
{
    // Flip game object horizontally
    public static void FlipHorizontal(this GameObject gameobject)
    {
        gameobject.transform.Rotate(new Vector3(0, 180, 0), Space.Self);
    }

    // Flip game object vertically
    public static void FlipVertical(this GameObject gameobject)
    {
        gameobject.transform.Rotate(new Vector3(180, 0, 0), Space.Self);
    }

    // Set a sprites transparency
    public static void SetAlpha(this SpriteRenderer sprite, float alphaLevel)
    {
        sprite.color = new Color(1, 1, 1, alphaLevel); 
    }


    // Returns the pixel position of a gameObject
    public static Vector2 PixelPosition(this Transform t, int ppu)
    {
        Vector2 pixelPos = t.position;
        pixelPos.x = Mathf.Round(pixelPos.x * ppu) / ppu;
        pixelPos.y = Mathf.Round(pixelPos.y * ppu) / ppu;
        return pixelPos;

    }


    // Returns whether the collider is above the specified collider.

    public static bool IsAbove(this Collider2D collider, Collider2D other)
    {
        return collider.bounds.min.y > other.bounds.max.y;
    }

    // Returns whether the collider is below the specified collider.

    public static bool IsBelow(this Collider2D collider, Collider2D other)
    {
        return collider.bounds.max.y < other.bounds.min.y;
    }


}
