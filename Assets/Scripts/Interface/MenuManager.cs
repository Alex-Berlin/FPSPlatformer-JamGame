using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Slider sensitivity;
    public static float mouseSensitivityModifier = 1f;

    private void Start()
    {
        sensitivity.value = mouseSensitivityModifier;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        mouseSensitivityModifier = sensitivity.value;
    }


}
