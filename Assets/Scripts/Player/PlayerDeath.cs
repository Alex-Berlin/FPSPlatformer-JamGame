using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 lastCheckpoint = Vector3.zero;

    private void Die()
    {
        GetComponent<Transform>().position = lastCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Checkpoint>() != null)
        {
            lastCheckpoint = other.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BulletMove>()!=null || collision.gameObject.GetComponent<Floor>() != null)
        {
            Die();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }
    }
}
