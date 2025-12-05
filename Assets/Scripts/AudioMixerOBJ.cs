using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerOBG : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private AudioMixer myMixer;

    private void Start()
    {
        if (AudioManager.Instance == null)
            return;

        myMixer = AudioManager.Instance.Mixer;

        float master = PlayerPrefs.GetFloat("masterVolume", 1f);
        float music = PlayerPrefs.GetFloat("musicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("sfxVolume", 1f);

        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;

        AudioManager.Instance.SetMasterVolume(master);
        AudioManager.Instance.SetMusicVolume(music);
        AudioManager.Instance.SetSFXVolume(sfx);

        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}



