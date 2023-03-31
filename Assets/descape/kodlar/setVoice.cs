using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class setVoice : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    [SerializeField] private TextMeshProUGUI ValueText;
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void on_changeVoice()
    {
        AudioListener.volume = Slider.value;
        ValueText.text = "%" + ((int)(Slider.value * 100)).ToString();
        Save();
    }
    public void Load()
    {
        Slider.value = PlayerPrefs.GetFloat("musicVolume");
        ValueText.text = PlayerPrefs.GetString("musicText");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", Slider.value);
        PlayerPrefs.SetString("musicText","%" + ((int)(Slider.value * 100)).ToString());
        ValueText.text = "%"+((int)(Slider.value * 100)).ToString();
    }
}
 