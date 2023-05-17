using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{
    private AgentAnimations agentAnimations;
    private AgentMover agentMover;

    private Animator animator;
    private AnimationEventHelper animationEventHelper;

    private Vector3 pointerInput, movementInput;

    public Vector3 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector3 MovementInput { get => movementInput; set => movementInput = value; }

    public GameObject healthBarUI;

    private void Awake()
    {
        animationEventHelper = GetComponentInChildren<AnimationEventHelper>();
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        animator = GetComponentInChildren<Animator>();
        agentMover = GetComponent<AgentMover>();
    }

    private void Update()
    {
        agentMover.MovementInput = MovementInput;
        healthBarUI.transform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public void PerformAttack()
    {
        animator.Play("Attack", -1, 0f);
        //Debug.Log("Attacking");
    }

    private void AnimateCharacter()
    {
        //Debug.Log("Animating");
        Vector3 lookDirection = pointerInput - transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(movementInput);
    }

}
