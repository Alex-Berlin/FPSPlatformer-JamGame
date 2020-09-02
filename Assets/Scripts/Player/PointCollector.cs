using UnityEngine;
using UnityEngine.UI;

public class PointCollector : MonoBehaviour
{
    private int scoreMax;
    private int currentScore = 0;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject winningText;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        scoreMax = FindObjectsOfType<Point>().Length;
        scoreText.text = $"{currentScore}/{scoreMax}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Point>() != null)
        {
            currentScore++;
            scoreText.text = $"{currentScore}/{scoreMax}";
            Destroy(other.gameObject);
            audioSource.Play();
            if (currentScore == scoreMax)
            {
                winningText.SetActive(true);
            }
        }
    }
}
