using UnityEngine;

// this class handles the BigMeteor meteor type
public class BigMeteor : MonoBehaviour, IMeteor 
{
    // interface variables
    public float distanceSquared { get; set; }
    public bool isMovingLeft { get; set; }
    public float speed { get; set; }

    // variables for pivot point (player)
    private GameObject pivot;
    private Vector2 pivotPosition;
    private Vector2 currentPosition;

    void Start()
    {
        // Randomly assign initial speed and direction when spawned
        speed = .05f;
        int direction = UnityEngine.Random.Range(0, 2);
        if (direction == 0)
            isMovingLeft = true;
        else
            isMovingLeft = false;
        // calculate distance
        pivot = GameManager.Instance._player;
        pivotPosition = GameManager.Instance._player.transform.position;
        currentPosition = transform.position;
        distanceSquared = (currentPosition - pivotPosition).sqrMagnitude;

        // speed logic
        speed *= distanceSquared;
    }

    void Update()
    {
        Move();
    }

    // movement logic
    public void Move()
    {
        // rotate towards player logic
        pivotPosition = pivot.transform.position;
        currentPosition = transform.position;
        
        // calculate direction and angle
        var direction = pivotPosition - currentPosition;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        
        // look at player if not already
        if (angle != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -angle);
        }

        // move left or right logic
        if (isMovingLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        
        // avoid player collision logic
        if ((currentPosition - pivotPosition).sqrMagnitude > distanceSquared)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if ((currentPosition - pivotPosition).sqrMagnitude < distanceSquared)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
}
