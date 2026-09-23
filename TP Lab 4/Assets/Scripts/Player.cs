using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject laserPrefab;

    private float speed = 6f;
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 6f;
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

    // Update is called once per frame
    void Update()
    {
        // Check if player is out-of-bounds of the screen & move them if needed.
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

        Shooting();
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

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
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
