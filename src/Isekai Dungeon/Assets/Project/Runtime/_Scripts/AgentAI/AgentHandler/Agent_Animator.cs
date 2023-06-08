using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Animator : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Animate_Idle()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Move", false);
    }

    public void Animate_Move()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", true);
    }

    public void Animate_Attack()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Move", false);
        animator.SetTrigger("OnAttack");
    }

    public void ResetTriggers()
    {
        animator.ResetTrigger("OnAttack");
    }

    public void FlipSprite(bool condition)
    {
        sprite.flipX = condition;
    }
}
