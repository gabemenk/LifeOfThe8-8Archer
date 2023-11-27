using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Volume : MonoBehaviour
{
    [SerializeField] public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        if (slider == null)
        {
            Debug.LogError("Slider is not assigned!");
            return;
        }

        // Set the slider value to the initial AudioListener.volume.
        slider.value = AudioListener.volume;
    }

    public void change()
    {
        // Set the AudioListener.volume to the current slider value.
        AudioListener.volume = slider.value;
    }
}
