using UnityEngine;

public class EasterEgg : MonoBehaviour, IPlatform
{
    public void ActivatePlatform()
    {
        GetComponent<AudioSource>().Play();
    }
}
