using System.Security.Cryptography;
using UnityEngine;

public class SwordAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement player;
    private Rigidbody rb;
    private bool isRunning = false;
    private bool isAttacking = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rb = player.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            isAttacking = false;
        }
        animator.SetBool("IsAirborne", player.Airborne);
        if (Input.GetAxisRaw("Horizontal") != 0|| Input.GetAxisRaw("Vertical") != 0)
        {
            isRunning = true;
            
        } else
        {
            isRunning = false;
        }
        animator.SetBool("IsRunning", isRunning);
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
        }
        animator.SetBool("IsAttacking", isAttacking);
    }

}
