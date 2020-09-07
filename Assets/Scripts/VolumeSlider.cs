using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // Logarithmic formula used is taken from 
    // https://www.youtube.com/watch?v=xNHSGMKtlv4
    // video. You should check his other videos out too!

    [SerializeField] private AudioMixer mixer;
    private Slider slider;
    private static float volume = 0.75f;
    private void Start()
    {
        if (TryGetComponent(out slider))
        {
            SetVolume();
        }

    }
    public void SetVolume()
    {
        mixer.SetFloat("Volume", Mathf.Log10(slider.value) * 20);
    }
}
