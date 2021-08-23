using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows the creation of a platform containing 3 parts.
// Hasa a left, middle and right sprite. middle sprite is
// repeated as size increases.

[ExecuteAlways] // Update in edit mode as well
public class CompositePlatform : MonoBehaviour
{
    public int size;
    public GameObject left, middle, right;
    GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        if (size < 2)
            size = 2;
        if (tiles == null)
            Create(size);
    }


    // Display platform in editor
    //void Update()
    //{
    //    if (!Application.isPlaying)
    //    {
    //        if (size < 2)
    //            size = 2;

    //        Create(size);
    //    }
    //}

    // create a platform of a given size
    private void Create(int size)
    {
        tiles = new GameObject[size];

        // initialize position of left tile
        Vector3 tilePos = new Vector3();
        tilePos = gameObject.transform.position;
        tilePos.x = gameObject.transform.position.x - (size / 2.0f);

        // set left tile
        tiles[0] = Instantiate(left, gameObject.transform);
        tiles[0].transform.position = tilePos;

        tilePos.x += 1.0f;

        // set middle tiles
        for (int i = 1; i < size - 1; i++)
        {
            tiles[i] = Instantiate(middle, gameObject.transform);
            tiles[i].transform.position = tilePos;
            tilePos.x += 1.0f;
        }

        // set right tile
        tiles[size - 1] = Instantiate(right, gameObject.transform);
        tiles[size - 1].transform.position = tilePos;
    }
}
