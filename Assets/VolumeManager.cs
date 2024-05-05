using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("volume", 1f);

        volumeSlider = GetComponent<Slider>();
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        AudioListener.volume = volumeSlider.value;
    }

}
