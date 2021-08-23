using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera follows the player

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public int ppu; // pixels per unit for scene
    public float verticalOffset = 1.0f; // offset camera vertically
    private Vector3 playerPos;

    // Update is called once per frame
    void LateUpdate()
    {
        // Get player's pixel position
        playerPos = player.transform.PixelPosition(16);

        // Set camera x and y to player and add vertical offset.
        transform.position = new Vector3(playerPos.x, playerPos.y + verticalOffset, transform.position.z);
    }

    // Round a given value to the nearest pixel value using the ppu.
    private float RoundToPixel(float x)
    {
        return Mathf.Round(x * ppu) / ppu;
    }

    public static bool isInView(Vector3 position)
    {
        Vector3 screenpoint = Camera.main.WorldToViewportPoint(position);
        if (screenpoint.y > 1 || screenpoint.y < 0)
            return false;
        else if (screenpoint.x > 1 || screenpoint.x < 0)
            return false;
        else
            return true;
    }
}
