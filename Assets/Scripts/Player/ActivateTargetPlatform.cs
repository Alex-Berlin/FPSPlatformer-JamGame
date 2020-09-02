using UnityEngine;

public class ActivateTargetPlatform : MonoBehaviour
{
    [SerializeField] private LayerMask mask = -1;
    [SerializeField] private MoveToHook player;
    [SerializeField] private float range = 30f;
    public static float Range { get; private set; }

    private void Start()
    {
        Range = range;
    }

    private void ActivatePlatform()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, range, mask);
        if (hit.collider!=null)
        {
            if (hit.collider.gameObject.GetComponent<IPlatform>() != null)
            {
                hit.collider.gameObject.GetComponent<IPlatform>().ActivatePlatform();
            } else if (hit.collider.gameObject.GetComponent<HookPoint>() != null)
            {
                player.accelerationPoint = hit.point;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ActivatePlatform();
        }
    }
}
