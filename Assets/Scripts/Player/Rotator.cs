using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Camera cam;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        transform.position = cam.transform.position;
        transform.eulerAngles = new Vector3 (0f, cam.transform.eulerAngles.y, 0f);
    }
}
