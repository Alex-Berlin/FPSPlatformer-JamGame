using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float deathTime = 7f;
    private float timer = 0;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + speed * transform.forward * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerDeath>() == null)
        {
            BulletPool.Instance.ReturnToPool(gameObject);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= deathTime)
        {
            BulletPool.Instance.ReturnToPool(gameObject);
        }
    }
}
