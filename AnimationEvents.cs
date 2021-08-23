using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents
{
    public UnityEvent OnAnimationFinished;

    public AnimationEvents()
    {
        OnAnimationFinished = new UnityEvent();
    }

}
