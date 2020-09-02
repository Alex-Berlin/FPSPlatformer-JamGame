
using UnityEngine;

public class TurretDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sword>() != null)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IPlatform>()!= null || collision.gameObject.GetComponent<BulletMove>() != null)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponentInChildren<LineRenderer>().enabled = false;
        GetComponentInChildren<TurretController>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
    }
}
