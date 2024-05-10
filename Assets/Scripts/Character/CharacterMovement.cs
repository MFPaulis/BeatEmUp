using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    private CharacterControls inputActions;
    private CharacterController characterController;
    private CharacterAnimations animations;

    // Walking
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float zSpeed = 1.5f;
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;

    // Jumping
    private float initialJumpVelocity;
    [SerializeField] private float maxJumpHeight = 1.0f;
    [SerializeField] private float maxJumpTime = 0.5f;

    private float groundedGravity = -.5f;
    private float gravity = -9.8f;

    private bool isJumpPressed = false;

    private void Awake()
    {
        inputActions = new CharacterControls();
        characterController = GetComponent<CharacterController>();
        animations = GetComponentInChildren<CharacterAnimations>();

        inputActions.Character.Movement.performed += OnMovement;
        inputActions.Character.Movement.canceled += OnMovement;
        inputActions.Character.Jump.started += OnJump;
        inputActions.Character.Jump.canceled += OnJump;

        SetupJumpVariables();
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        Debug.Log(gravity);
        Debug.Log(initialJumpVelocity);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement = new Vector3(currentMovementInput.x * walkSpeed, currentMovement.y, currentMovementInput.y * zSpeed);
        
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void HandleAnimatios()
    {
        animations.Walk(currentMovementInput.x != 0 || currentMovementInput.y != 0);
    }

    void RotatePlayer()
    {
        if (currentMovementInput.x > 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if(currentMovementInput.x < 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + gravity * Time.deltaTime;
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            currentMovement.y = nextYVelocity;
        }
    }

    private void HandleJump()
    {
        if(characterController.isGrounded && isJumpPressed)
        {
            currentMovement.y = initialJumpVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        HandleAnimatios();
        HandleGravity();
        HandleJump();

        characterController.Move(Time.deltaTime * currentMovement);
    }
}
