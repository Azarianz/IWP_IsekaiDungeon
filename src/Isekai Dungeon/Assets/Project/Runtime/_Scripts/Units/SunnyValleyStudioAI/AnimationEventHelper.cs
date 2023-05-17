using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackPeformed, OnAttackFrame;

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPeformed?.Invoke();
    }

    public void TriggerDMG()
    {
        OnAttackFrame?.Invoke();
    }
}
