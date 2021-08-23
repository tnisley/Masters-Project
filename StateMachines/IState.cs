using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter();

    void OnExit();

    void StateUpdate();

    void StateFixedUpdate();
    
    // used to unsubscribe from events
    void Destroy();

}
