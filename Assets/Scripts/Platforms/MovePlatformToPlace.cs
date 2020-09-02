using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlatformToPlace : MonoBehaviour, IPlatform
{
    //This platform stays in place until ActivatePlatform() is called. After which it moves to it's target location and stays there until next call.

    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    private bool isMoving = false;
    private bool isReturning = false;
    [SerializeField] private Transform objectToMove;
    private Vector3 pointToMove;
    [SerializeField] private float standardMoveDistance = 5f;
    private Vector3 startingPosition;
    [SerializeField] [Range(0.01f, 1f)] private float returnMod = 0.25f;

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
        }
        else
        {
            pointToMove = transform.position + transform.up * standardMoveDistance;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (!isReturning)
            {
                MovePlatform(pointToMove);
            }
            else if (isReturning)
            {
                MovePlatform(startingPosition);
            }
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, pointToMove) < 0.1f && !isReturning)
            {
                isReturning = true;
                isMoving = false;
            }
            else if (Vector3.Distance(transform.position, startingPosition) < 0.1f && isReturning)
            {
                isReturning = false;
                isMoving = false;
            }
        }
    }

    private void MovePlatform(Vector3 target)
    {
        if (isMoving)
        {
            rb.MovePosition(transform.position + (target - transform.position).normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void ActivatePlatform()
    {
        isMoving = true;
    }
}
