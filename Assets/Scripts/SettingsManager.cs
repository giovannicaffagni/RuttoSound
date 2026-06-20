using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Microfono")]
    public Slider micSlider;
    public TMP_Text micValueText;

    [Header("Volume")]
    public Slider volumeSlider;
    public TMP_Text volumeValueText;


    void Start()
    {
        // Carica impostazioni salvate
        micSlider.value = PlayerPrefs.GetFloat("MicSensitivity", 0.7f);
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.85f);

        // Aggiorna le percentuali a schermo
        UpdateMicText();
        UpdateVolumeText();
    }


    // ==========================
    // MICROFONO
    // ==========================

    public void UpdateMicText()
    {
        int percentage = Mathf.RoundToInt(micSlider.value * 100);

        micValueText.text = percentage + "%";
    }


    // ==========================
    // VOLUME
    // ==========================

    public void UpdateVolumeText()
    {
        int percentage = Mathf.RoundToInt(volumeSlider.value * 100);

        volumeValueText.text = percentage + "%";
    }


    // ==========================
    // SALVATAGGIO
    // ==========================

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MicSensitivity", micSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);

        PlayerPrefs.Save();

        Debug.Log("Impostazioni salvate!");
    }


    // ==========================
    // CALIBRAZIONE
    // ==========================

    public void CalibrateMic()
    {
        Debug.Log("Calibrazione microfono...");
    }

}