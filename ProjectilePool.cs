using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object pool system for projectiles. 

public class ProjectilePool
{
    private List<Projectile> pool;
    public int poolSize;
    Projectile projectile;


    public ProjectilePool(int poolSize, Projectile p)
    {
        this.poolSize = poolSize;
        pool = new List<Projectile>();
        projectile = p;
    }

    private Projectile Add()
    {
        Debug.Log("Adding Projectile to pool");

        Projectile clone = Object.Instantiate(projectile);
        pool.Add(clone);
        return clone;
    }

    // Is an object available
    public bool HasNext()
    {
        if (pool != null)
        {
            foreach (Projectile p in pool)
                if (!p.gameObject.activeInHierarchy)
                    return true;
            if (pool.Count < poolSize)
                return true;
        }
        
            return false;
    }

    // Get next available object in pool
    public Projectile Get()
    {
        foreach (Projectile p in pool)
        {
            if (!p.gameObject.activeInHierarchy)
            {
                Debug.Log("Projectile Found!");
                p.gameObject.SetActive(true);
                return p;
            }
        }

        // If no available object, create new one if pool limit not reached
        if (pool.Count < poolSize)
        {
            Projectile p = Add();
            //obj.SetActive(true);
            return p;
        }

        else
        {
            return null;
        }
    }

    public int Size()
    {
        return poolSize;
    }
}
