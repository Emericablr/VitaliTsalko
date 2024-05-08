using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider volumeSlider;

    public float MinVolume = -80;
    public float MaxVolume = 20;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();

        volumeSlider.maxValue = MaxVolume;
        volumeSlider.minValue = MinVolume;
    }

    public void UpdateVolume()
    {
        mixer.SetFloat("Volume", volumeSlider.value);
    }
}
