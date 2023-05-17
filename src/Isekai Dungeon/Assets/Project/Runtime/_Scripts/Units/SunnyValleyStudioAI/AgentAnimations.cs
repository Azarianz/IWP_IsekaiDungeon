using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RotateToPointer(Vector2 lookDirection)
    {
        Vector3 scale = transform.localScale;
        if (lookDirection.x > 0)
        {
            scale.x = 1;
        }
        else if (lookDirection.x < 0)
        {
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    public void PlayAnimation(Vector3 movementInput)
    {
        animator.SetBool("Move", movementInput.magnitude > 0);
    }

    public void setDefault()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Move", false);
    }
}
