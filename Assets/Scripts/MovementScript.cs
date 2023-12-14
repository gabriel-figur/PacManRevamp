using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }
    public float movementSpeed = 9.0f;
    public float movementSpeedMultiplier = 1.0f;
    public Vector2 primeDirection;
    public LayerMask wallLayer;
    public Vector2 currentDirection { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 initialPos { get; private set; }
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
    }

    private void Start()
    {
        ResetMovement();
    }

    public void ResetMovement()
    {
        currentDirection = primeDirection;
        nextDirection = Vector2.zero;
        transform.position = initialPos;
        rigidBody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = rigidBody.position;
        Vector2 movement = currentDirection * (movementSpeed * movementSpeedMultiplier * Time.fixedDeltaTime);
        rigidBody.MovePosition(currentPosition + movement);
    }

    public void SetDirection(Vector2 directionSet, bool forced = false)
    {
        if (!DirectionInvalid(directionSet))
        {
            currentDirection = directionSet;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = directionSet;
        }
    }

    private bool DirectionInvalid(Vector2 directionSet)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f,
                                            directionSet, 1.5f, wallLayer);
        return hit.collider != null;
    }
}
