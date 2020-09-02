using UnityEngine;
using UnityEngine.UI;

public class TargetColoring : MonoBehaviour
{
    [SerializeField] private Image target;
    [SerializeField] private Color colorDefault;
    [SerializeField] private Color colorSelected;
    [SerializeField] private LayerMask mask = -1;

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, ActivateTargetPlatform.Range, mask);
        if (hit.collider != null && (hit.collider.gameObject.GetComponent<IPlatform>() != null || hit.collider.gameObject.GetComponent<HookPoint>()!= null))
        {
            target.color = colorSelected;
        } else
        {
            target.color = colorDefault;
        }
    }
}
