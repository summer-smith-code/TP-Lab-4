using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMeteor : MonoBehaviour, IMeteor 
{
    public Vector3 direction { get; set; }

    [SerializeField] public float distanceSquared { get; set; }
    [SerializeField] public bool isMovingLeft { get; set; }

    [SerializeField] public float speed { get; set; }
    [SerializeField] private GameObject pivot;

    private Vector2 pivotPosition;
    private Vector2 currentPosition;

    void Start()
    {
        // calculate distance
        pivotPosition = pivot.transform.position;
        currentPosition = transform.position;
        distanceSquared = (currentPosition - pivotPosition).sqrMagnitude;

        // speed logic
        speed *= distanceSquared;
    }

    void Update()
    {
        Move();
    }

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


    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
            Destroy(whatIHit.gameObject);
        }
        else if (whatIHit.tag == "Laser")
        {
            Destroy(whatIHit.gameObject);
        }
    }
}
