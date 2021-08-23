using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stores a reference to objects physics material

public class ObjectMaterial : MonoBehaviour
{
    [SerializeField]
    private PhysicsMaterial material;

    public PhysicsMaterial Get()
    {
        return material;
    }
}
