using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    //[SerializeField]
    //private InputActionReference movement, attack, pointerPosition;

    //private void Update()
    //{
    //    OnMovementInput?.Invoke(movement.action.ReadValue<vector3>().normalized);
    //    OnPointerInput?.Invoke(GetPointerInput());
    //}

    //private Vector3 GetPointerInput()
    //{
    //    Vector3 mousePos = pointerPosition.action.ReadValue<vector3>();
    //    mousePos.z = Camera.main.nearClipPlane;
    //    return Camera.main.ScreenToWorldPoint(mousePos);
        
    //}

    //private void OnEnable()
    //{
    //    attack.action.performed += PerformAttack;
    //}

    //private void PerformAttack(InputAction.CallbackContext obj)
    //{
    //    OnAttack?.Invoke();
    //}

    //private void OnDisable()
    //{
    //    attack.action.performed -= PerformAttack;
    //}
}
