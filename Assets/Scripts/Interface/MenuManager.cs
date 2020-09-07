using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Slider sensitivity;
    public static float mouseSensitivityModifier = 1f;

    private void Start()
    {
        if (sensitivity != null)
        {
            sensitivity.value = mouseSensitivityModifier;
        }
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

    public void SetSensetivity()
    {
        mouseSensitivityModifier = sensitivity.value;
    }


}
