using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEvents
{
    public UnityEvent OnEnemyDamage;
    public UnityEvent OnEnemyDeath;
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;


    public EnemyEvents()
    {
        OnEnemyDamage = new UnityEvent();
        OnEnemyDeath = new UnityEvent();
    }
}
