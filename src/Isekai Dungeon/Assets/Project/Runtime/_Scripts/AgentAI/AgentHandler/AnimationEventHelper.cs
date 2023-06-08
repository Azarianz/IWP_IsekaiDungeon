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

    public void Attack_Finish()
    {
        OnAttackPeformed?.Invoke();
    }

    public void Attack_Frame()
    {
        OnAttackFrame?.Invoke();
    }
}
