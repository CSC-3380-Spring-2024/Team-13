using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    //Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

    public void ChangeVolume() 
    {
        AudioListener.volume = volumeSlider.value; //change volume 
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicvolume"); //grab value of slider
    }

    private void Save() 
    {
        PlayerPrefs.SetFloat("musicvolume", volumeSlider.value); //save the value of the slider
    }
}
