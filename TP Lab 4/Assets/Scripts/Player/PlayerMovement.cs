using UnityEngine;
using UnityEngine.InputSystem;

// This script handles the player's movement.
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    
    // Variables for handling input system.
    private PlayerInputActions _playerInputActions;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    // On enable, gets necessary references & enables input.
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    // On disable, disables input.
    private void OnDisable() { _playerInputActions.Player.Disable(); }

    // Applies physics-based movement to the player using input system values.
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveInput.x * moveSpeed, _moveInput.y * moveSpeed);
    }

    // Stores movement input for the player; automatically called by input system.
    private void OnMove(InputValue value) { _moveInput = value.Get<Vector2>(); }
}
