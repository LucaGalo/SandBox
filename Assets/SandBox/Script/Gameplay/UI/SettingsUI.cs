using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;

    [SerializeField] AudioMixer mixer;

    private void Awake()
    {
        masterVolume.onValueChanged.AddListener((value) => SetVolume("Master", value));
        musicVolume.onValueChanged.AddListener((value) => SetVolume("Music", value));
        sfxVolume.onValueChanged.AddListener((value) => SetVolume("SFX", value));
    }

    private void OnEnable()
    {
        mixer.GetFloat("Master", out float masterValue);
        masterVolume.SetValueWithoutNotify(Mathf.Pow(10f, masterValue / 20f));

        mixer.GetFloat("Music", out float musicValue);
        musicVolume.SetValueWithoutNotify(Mathf.Pow(10f, musicValue / 20f));

        mixer.GetFloat("SFX", out float sfxValue);
        sfxVolume.SetValueWithoutNotify(Mathf.Pow(10f, sfxValue / 20f));
    }

    void SetVolume(string group, float volume)
    {
        var db = ValueToVolume(volume);
        mixer.SetFloat(group, db);
        mixer.GetFloat(group, out float sfxValue);
    }

    private float ValueToVolume(float value)
    {
        return Mathf.Log10(value) * 20f;
    }
}
