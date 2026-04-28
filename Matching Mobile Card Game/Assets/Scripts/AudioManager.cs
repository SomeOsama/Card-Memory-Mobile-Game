using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    // Sliders
    [Header("Audio Sliders (0.0 – 1.0)")]
    public Slider musicSlider;
    public Slider sfxSlider;

    // Player preferences for consistancy across sessions
    private const string KEY_MUSIC = "Setting_MusicVolume";
    private const string KEY_SFX = "Setting_SFXVolume";

    // Default values when no saved settings exist
    private const float DEFAULT_VOLUME = 0.8f;


    private void Awake()
    {
        // Validate required references
        if (audioMixer == null)
            Debug.LogWarning("[AudioManager] AudioMixer is not assigned. Audio settings will not work.");
    }

    private void Start()
    {
        LoadSettings();
        RegisterSliderCallbacks();
    }


    // Loads saved settings or defaults and applies them to sliders and the scene
    private void LoadSettings()
    {
        float music = PlayerPrefs.GetFloat(KEY_MUSIC, DEFAULT_VOLUME);
        float sfx = PlayerPrefs.GetFloat(KEY_SFX, DEFAULT_VOLUME);

        // Apply to sliders
        if (musicSlider) musicSlider.value = music;
        if (sfxSlider) sfxSlider.value = sfx;
    }

    // OnValueChanged event is connected to the appropriate handler
    private void RegisterSliderCallbacks()
    {
        if (musicSlider) musicSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxSlider) sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Sets music volume
    public void SetMusicVolume(float value)
    {
        ApplyMixerVolume("MusicVolume", value);
        PlayerPrefs.SetFloat(KEY_MUSIC, value);
    }

    // Sets SFX volume
    public void SetSFXVolume(float value)
    {
        ApplyMixerVolume("SFXVolume", value);
        PlayerPrefs.SetFloat(KEY_SFX, value);
    }

    private void ApplyMixerVolume(string parameterName, float linearValue)
    {
        if (audioMixer == null) return;
        float dB = linearValue > 0.0001f
            ? Mathf.Log10(linearValue) * 20f
            : -80f;

        audioMixer.SetFloat(parameterName, dB);
    }

    // Resets all settings to the default values
    public void ResetToDefaults()
    {
        if (musicSlider) musicSlider.value = DEFAULT_VOLUME;
        if (sfxSlider) sfxSlider.value = DEFAULT_VOLUME;

        PlayerPrefs.Save();
        Debug.Log("[AudioManager] Settings reset to defaults.");
    }

    // Explicitly saves all current settings to PlayerPrefs.
    // The settings are also auto-saved whenever a slider moves, but
    // this was added for users who like to make sure their settings
    // will actually save
    public void SaveSettings()
    {
        PlayerPrefs.Save();
        Debug.Log("[AudioManager] Settings saved.");
    }

    private void OnDestroy()
    {
        // Clean up listeners
        if (musicSlider) musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        if (sfxSlider) sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
