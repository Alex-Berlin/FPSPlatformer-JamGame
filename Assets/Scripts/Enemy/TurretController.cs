using UnityEngine;

public class TurretController : MonoBehaviour
{
    /******************************/
    [Header("Idle")]
    private Quaternion startingRotation;
    private float currentOffset;
    [SerializeField] private float idleSpeed = 15f;
    [SerializeField] private float idleRotation = 30f;
    /******************************/
    [Header("Detection")]
    [SerializeField] private float detectionDistance = 23f;
    [SerializeField] private float detectionRotation = 45f;
    [SerializeField] private float timeToDetection = 1f;
    private float currentDetectionTime = 0;
    private Transform playerPosition;
    [SerializeField] private LayerMask layer;
    /******************************/
    [Header("Attack")]
    private bool isActive = false;
    [SerializeField] private Transform barrelEnd;
    [SerializeField] private float reloadSpeed = 0.5f;
    private float currentReload = 0;
    /*****************************/
    //Audio
    private AudioSource shootSound;

    private void Start()
    {
        startingRotation = transform.rotation;
        playerPosition = FindObjectOfType<PlayerMovement>().transform;
        shootSound = GetComponent<AudioSource>();
    }

    private void CheckForPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, playerPosition.position - transform.position, out hit, detectionDistance, layer);
        if (hit.collider != null && hit.collider.GetComponent<PlayerMovement>() != null 
            && Vector3.Angle(playerPosition.position - transform.position, transform.forward) <= detectionRotation)
        {
            if (currentDetectionTime < timeToDetection)
            {
                currentDetectionTime += Time.deltaTime;
            } else
            {
                isActive = true;
            }
        }
        else
        {
            currentDetectionTime = 0;
            isActive = false;
        }
    }

    private void Update()
    {
        CheckForPlayer();
        if (isActive)
        {
            Attack();
        } else if (!isActive)
        {
            Idle();
        }
    }

    private void Attack()
    {
        transform.LookAt(playerPosition, Vector3.up);
        if (currentReload < reloadSpeed)
        {
            currentReload += Time.deltaTime;
        } else 
        { 
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = BulletPool.Instance.Get();
        bullet.transform.position = barrelEnd.position;
        bullet.transform.rotation = barrelEnd.rotation;
        bullet.gameObject.SetActive(true);
        shootSound.Play();
        currentReload = 0;
    }

    private void Idle()
    {
        currentOffset = Mathf.PingPong(Time.time * idleSpeed, idleRotation * 2) - idleRotation;
        transform.rotation = Quaternion.AngleAxis(startingRotation.eulerAngles.y + currentOffset, Vector3.up);
    }

}