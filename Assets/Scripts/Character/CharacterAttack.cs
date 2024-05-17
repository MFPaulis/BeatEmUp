using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2,
}

public class CharacterAttack : MonoBehaviour
{
    private CharacterAnimations animations;
    private CharacterControls inputActions;

    [SerializeField] private float defaultComboTimer = 0.4f;
    private float currentComboTimer;
    private ComboState currentComboState;
    private bool activateTimerToReset = false;

    private void Awake()
    {
        animations = GetComponentInChildren<CharacterAnimations>();
    }

    private void Start()
    {
        inputActions = GetComponent<CharacterMovement>().GetInputActions();
        inputActions.Character.Punch.performed += OnPunch;

        currentComboTimer = defaultComboTimer;
        currentComboState = ComboState.NONE;
    }

    private void OnPunch(InputAction.CallbackContext context)
    {
        currentComboState++;
        activateTimerToReset = true;
        currentComboTimer = defaultComboTimer;

        switch (currentComboState)
        {
            case ComboState.PUNCH_1: animations.Punch_1(); break;
            case ComboState.PUNCH_2: animations.Punch_2(); break;
            case ComboState.PUNCH_3: 
                animations.Punch_3();
                currentComboState = ComboState.NONE;
                break;
        }
    }

    private void ResetComboState()
    {
        if(activateTimerToReset) 
        {
            currentComboTimer -= Time.deltaTime;

            if(currentComboTimer <= 0)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;
            }
        }
    }

    private void Update()
    {
        ResetComboState();
    }
}
