using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// This script handles the player's attacks.
public class PlayerAttack : MonoBehaviour, IAttacker 
{
    [SerializeField] 
    private GameObject laserPrefab;
    public GameObject attackPrefab
    {
        get { return laserPrefab; }
        set { laserPrefab = value; }
    }
    
    [SerializeField] 
    private float laserCooldown = 1f;
    public float attackCooldown
    {
        get { return laserCooldown; }
        set { laserCooldown = value; }
    }
    
    private Coroutine _shootCooldown;
    
    // Calls Attack() via input system event.
    private void OnShoot(InputValue value) { if (value.isPressed && _shootCooldown == null) Attack(); }

    // Creates the laser and begins cooldown coroutine.
    public void Attack()
    {
        Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        _shootCooldown = StartCoroutine(Cooldown());
    }

    // Waits for the given amount of time before setting self reference to null.
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(laserCooldown);
        _shootCooldown = null;
    }
}
