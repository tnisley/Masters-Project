using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Library of useful static game functions

public class Utilities
{
    public static float RoundToPixel(float value, int ppu)
    {
        return (Mathf.Floor(value * ppu)) / ppu;
    }

    public static Vector2 RoundToPixel(Vector2 value, int ppu)
    {
        return new Vector2(RoundToPixel(value.x, ppu), RoundToPixel(value.y, ppu));
    }
}
