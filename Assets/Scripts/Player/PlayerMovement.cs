using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Rotator rotator;
    //Movement
    [Header("Running and jumping")]
    [SerializeField] private float moveSpeed = 500f;
    private Vector3 moveDir = new Vector3();
    private Vector3 lastDir = new Vector3();
    //Jumping
    [SerializeField] private float jumpHeight = 1000f;
    [SerializeField] private float gravityModifier = 750f;
    [SerializeField] [Range(0f, 1f)] private float airControlMod = 0.1f;
    //Ground Check
    private CapsuleCollider capsule;
    private bool hasJumped = false;
    public bool Airborne { get; private set; } //for animator
    //Jump Mod
    [Header("Jump height modifier")]
    //The longer the run - the higher is jump
    [SerializeField] [Range(0f, 10f)] private float timeToMaxSpeed = 3f;
    [SerializeField] [Range(0f, 1f)] private float minMod = 0.3f;
    private float currentTimeRunning;
    //Audio
    private AudioSource stepsSound;
    private bool isPlaying = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotator = FindObjectOfType<Rotator>();
        capsule = GetComponent<CapsuleCollider>();
        stepsSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //move imput and direction
        moveDir = (rotator.transform.forward * Input.GetAxisRaw("Vertical") + rotator.transform.right * Input.GetAxisRaw("Horizontal")).normalized;
        //jumping imput and ground check
        if (isGrounded() && !hasJumped)
        {
            lastDir = moveDir;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasJumped = true;
            }
        }
        Airborne = !isGrounded();

        //jump modifier
        if (moveDir == Vector3.zero) { currentTimeRunning = 0; }
        else { CalculateModifier(); }

        //play steps sound
        if (isGrounded() && moveDir != Vector3.zero && !isPlaying)
        {
            stepsSound.Play();
            isPlaying = true;
        } else if (!isGrounded() || moveDir == Vector3.zero)
        { 
            stepsSound.Stop();
            isPlaying = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(rotator.transform.rotation);

        if (rb.velocity.y <= 0 && !isGrounded())
        {
            rb.AddForce(-gravityModifier * transform.up * Time.fixedDeltaTime, ForceMode.Acceleration);
        }

        if (isGrounded())
        {
            rb.AddForce(moveDir * moveSpeed * Time.fixedDeltaTime);
        } else
        {
            rb.AddForce(lastDir * moveSpeed * Time.fixedDeltaTime * (1-airControlMod));
            rb.AddForce(moveDir * moveSpeed * airControlMod * Time.fixedDeltaTime);
        }

        if (hasJumped)
        {
            rb.AddForce(Vector3.up * jumpHeight * CalculateModifier());
            hasJumped = false;
        }
    }


    #region Ground check
    private bool isGrounded()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out hit, capsule.height/2);
        return hit.collider != null;
    }
    #endregion

    private float CalculateModifier() 
    {
        currentTimeRunning += Time.deltaTime;
        float lerpMod = currentTimeRunning / timeToMaxSpeed;
        lerpMod = Mathf.Clamp01(lerpMod);
        float currentMod = Mathf.Lerp(minMod, 1f, lerpMod);
        return currentMod;
    }


}
