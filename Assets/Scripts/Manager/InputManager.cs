using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [SerializeField] float minimumInputThreshold = 0.1f;
    private InputSystem_Actions playerInput;
    public event EventHandler OnAttackPerformed;
    public event EventHandler OnAttackReleased;
    public event EventHandler OnInteract;
    public event EventHandler OnInventory;
    
    public enum InputActions
    {
        NONE,
        MOVE,
        ATTACK,
        INTERACT,
        DASH,
        ABILITY,
        INVENTORY
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of InputManager detected.");
        }

        Instance = this;

        playerInput = new InputSystem_Actions();
        playerInput.Enable();
        
        playerInput.Player.Attack.started += PlayerAttackStarted;
        playerInput.Player.Attack.canceled += PlayerAttackReleased;
        playerInput.Player.Interact.started += PlayerInteractStarted;
        playerInput.Player.ToggleInventory.started += PlayerInventoryStarted;
    }

    private void PlayerInventoryStarted(InputAction.CallbackContext obj)
    {
        OnInventory?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerInteractStarted(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerAttackReleased(InputAction.CallbackContext obj)
    {
        OnAttackReleased?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerAttackStarted(InputAction.CallbackContext obj)
    {
        OnAttackPerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementDirection()
    {
        Vector2 input = playerInput.Player.Move.ReadValue<Vector2>();
        
        if (input.magnitude < minimumInputThreshold)
        {
            return Vector2.zero;
        }
        
        return playerInput.Player.Move.ReadValue<Vector2>();
    }

}
