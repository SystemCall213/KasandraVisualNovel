using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider typingSpeedSlider;
    [SerializeField] private Button back;
    [SerializeField] private AudioSource audioSource;

    public event Action OnTypingSpeedChanged;

    private const string VolumeKey = "MasterVolume";
    private const string TypingSpeedKey = "TypingSpeed";

    private void Awake()
    {
        back.onClick.AddListener(CloseSettings);
    }

    void Start()
    {
        // Load settings
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        float savedTypingSpeed = PlayerPrefs.GetFloat(TypingSpeedKey, 0.25f);

        float invertedValue = typingSpeedSlider.maxValue - savedTypingSpeed;

        volumeSlider.value = savedVolume;
        typingSpeedSlider.value = invertedValue;

        audioSource.volume = savedVolume;
        GameSettings.TypingSpeed = savedTypingSpeed;
        OnTypingSpeedChanged?.Invoke();

        // Add listeners
        volumeSlider.onValueChanged.AddListener(SetVolume);
        typingSpeedSlider.onValueChanged.AddListener(SetTypingSpeed);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        #if UNITY_EDITOR
            PlayerPrefs.Save();
        #endif
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
    }

    public void SetTypingSpeed(float value)
    {
        float invertedValue = typingSpeedSlider.maxValue - value;
        GameSettings.TypingSpeed = invertedValue;
        OnTypingSpeedChanged?.Invoke();
        PlayerPrefs.SetFloat(TypingSpeedKey, invertedValue);
    }
}
