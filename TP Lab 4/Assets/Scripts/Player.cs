using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject laserPrefab;

    private float speed = 6f;
    private bool canShoot = true;

    // Variables for handling input system.
    private PlayerInputActions _playerInputActions;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    // On enable, gets necessary references & enables input.
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    // On disable, disables input.
    void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }

    // Applies physics-based movement to the player using input system values.
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveInput.x * speed, _moveInput.y * speed);
    }

    // Stores movement input for the player; automatically called by input system.
    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    // Calls Shooting() via input system event.
    private void OnShoot(InputValue value)
    {
        if (value.isPressed) Shooting();
    }

    void Shooting()
    {
        if (canShoot)
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            canShoot = false;
            StartCoroutine("Cooldown");
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
