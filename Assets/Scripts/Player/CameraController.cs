using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    //Player follow values
    private Vector3 offset;
    private PlayerMovement player;

    [SerializeField] [Range(1f,300f)] private float sensitivity = 50f;
    private float mouseX, mouseY;
    [SerializeField][Range(-90f,30f)] private float xMinRotation = -80f;
    [SerializeField][Range(80f,100f)] private float xMaxRotation = 90f;
    private Camera cam;


    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        offset = player.transform.position - transform.position;

        cam = GetComponent<Camera>();
        mouseX = transform.eulerAngles.x;
        mouseY = transform.eulerAngles.y;
    }
    private void Update()
    {
        mouseX += Input.GetAxis("Mouse Y") * sensitivity * MenuManager.mouseSensitivityModifier * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse X") * sensitivity * MenuManager.mouseSensitivityModifier * Time.deltaTime;
        //Clamp x-axis rotation value.
        mouseX = Mathf.Clamp(mouseX, xMinRotation, xMaxRotation);
        //Apply camera rotation.
        cam.transform.rotation = Quaternion.Euler(-mouseX, mouseY, 0);
    }

    private void LateUpdate()
    {
        //Move camera to player position.
        cam.transform.position = player.transform.position - offset; 
    }
}
