using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlatformUp : MonoBehaviour, IPlatform
{
    //This platform stays in place until ActivatePlatform() is called, after which it moves to target position and slowly returns back.

    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 30f;
    private bool isMoving = false;
    [SerializeField] private Transform objectToMove;
    private Vector3 pointToMove;
    [SerializeField] private float standardMoveDistance = 5f;
    private Vector3 startingPosition;
    private float moveDistance;
    private bool isReturning = false;
    [SerializeField][Range(0.01f, 1f)] private float returnMod = 0.25f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        startingPosition = transform.position;
        if (objectToMove != null)
        {
            pointToMove = objectToMove.position;
        } else
        {
            pointToMove = transform.position + transform.up * standardMoveDistance;
        }
        moveDistance = Vector3.Distance(startingPosition, pointToMove);
    }
    /// <summary>
    /// Moves platform with kinematic rigidbody to target position.
    /// </summary>
    /// <param name="target">Position to move.</param>
    /// <param name="mod">Speed modifier.</param>
    private void MovePlatform(Vector3 target, float mod)
    {
        rb.MovePosition(transform.position + (target - transform.position).normalized * moveSpeed * Time.fixedDeltaTime * mod);
    }
    public void ActivatePlatform()
    {
        if (!isMoving && !isReturning)
        {
            startingPosition = transform.position;
            isMoving = true;
        }
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            MovePlatform(pointToMove, 1f);
        } else if (isReturning)
        {
            MovePlatform(startingPosition, returnMod);
        }

    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, startingPosition) >= moveDistance && isMoving)
        {
            isMoving = false;
            transform.position = pointToMove;
            isReturning = true;
        }
        else if (Vector3.Distance(transform.position, pointToMove) >= moveDistance && isReturning)
        {
            isReturning = false;
            transform.position = startingPosition;
        }

    }
}
