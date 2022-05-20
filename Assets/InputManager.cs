using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; protected set;}
    private void Awake() {
        if(Instance != this && Instance != null){
            Destroy(this);
        }
        Instance = this;
        inputActions = new();

        inputActions.Player.Move.started += ctx => moveDirection = ctx.ReadValue<float>();

        
        inputActions.Player.Move.canceled += ctx => moveDirection = 0;

        inputActions.Player.Jump.canceled += _ => flagJump = true;
        inputActions.Player.Jump.performed += _ => flagHighJump = true;
    }

    public InputActions inputActions;

    [Header("Input Flags")]
    public float moveDirection;
    public bool flagJump;
    public bool flagHighJump;

    private void OnEnable() {
        inputActions.Enable();
    }
    private void OnDisable() {
        inputActions.Disable();
    }

}
