using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlatformFixed : MonoBehaviour, IPlatform
{
    //This platform continously moves from a to b and can be stopped by ActivatePlatform().

    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f;
    private bool isMoving = true;
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
        if (Vector3.Distance(transform.position, pointToMove) < 0.1f && !isReturning)
        {
            isReturning = true;
        }
        else if (Vector3.Distance(transform.position, startingPosition) < 0.1f && isReturning)
        {
            isReturning = false;
        }
    }

    private void MovePlatform(Vector3 target)
    {
        rb.MovePosition(transform.position + (target - transform.position).normalized * moveSpeed * Time.fixedDeltaTime);
    }

    public void ActivatePlatform()
    {
        isMoving = !isMoving;
    }
}