using UnityEngine;

public class OpenCloseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            menu.SetActive(!menu.activeSelf);
        }
    }
}
