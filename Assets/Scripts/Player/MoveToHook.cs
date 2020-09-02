using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveToHook : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 500;

    private bool hasSword = true;
    [SerializeField] private Transform sword;
    private Quaternion swordStartingRot;

    [SerializeField] private Transform hookStart;
    private LineRenderer lr;
    public Vector3 accelerationPoint { private get; set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        accelerationPoint = Vector3.zero;
        lr = hookStart.GetComponent<LineRenderer>();
        swordStartingRot = sword.localRotation;
        
    }

    private void Move(Vector3 target)
    {
        rb.AddForce((target - transform.position).normalized * speed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    private void FixedUpdate()
    {
        if (accelerationPoint != Vector3.zero && hasSword)
        {
            Move(accelerationPoint);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            accelerationPoint = Vector3.zero;
            sword.localRotation = swordStartingRot;
        }
        if (hasSword && accelerationPoint != Vector3.zero)
        {
            RotateSword();
        }
    }

    private void LateUpdate()
    {
        if (hasSword)
        {
            RenderRope();
        }
    }


    private void RenderRope()
    {

        if (accelerationPoint == Vector3.zero)
        {
            lr.positionCount = 0;
        } else
        {
            lr.positionCount = 2;
            lr.SetPosition(0, hookStart.position);
            lr.SetPosition(1, accelerationPoint);
        }
    }

    private void RotateSword()
    {
        sword.LookAt(accelerationPoint);
    }
}
