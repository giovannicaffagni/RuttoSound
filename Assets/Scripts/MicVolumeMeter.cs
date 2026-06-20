using UnityEngine;
using UnityEngine.UI;

public class MicVolumeMeter : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Trascina qui la Slider che fa da barra del volume")]
    public Slider volumeBar;

    [Header("Impostazioni microfono")]
    [Tooltip("Lascia vuoto per usare il microfono di default")]
    public string microphoneDevice;

    [Tooltip("Quanto è sensibile la barra (valori più alti = si riempie più facilmente)")]
    public float sensitivity = 20f;

    [Tooltip("Quanto velocemente la barra sale/scende seguendo il volume")]
    public float smoothing = 10f;

    [Header("Noise Gate")]
    [Tooltip("Soglia minima (0-1) sotto la quale il suono viene ignorato. Modificabile a runtime, es. dalle opzioni di gioco.")]
    [Range(0f, 1f)]
    public float noiseGateThreshold = 0.05f;

    public bool IsAboveGate { get; private set; }

    private AudioClip micClip;
    private const int sampleWindow = 128;
    private float currentVolume;

    void Start()
    {
        if (Microphone.devices.Length == 0)
        {
            Debug.LogWarning("Nessun microfono trovato.");
            enabled = false;
            return;
        }

        if (string.IsNullOrEmpty(microphoneDevice))
            microphoneDevice = Microphone.devices[0];

        Debug.Log("Uso il microfono: " + microphoneDevice);

        // Registra in loop, 1 secondo di buffer, a 44100 Hz
        micClip = Microphone.Start(microphoneDevice, true, 1, 44100);
    }

    void Update()
    {
        if (micClip == null) return;

        float volume = GetLoudness();

        // Noise gate: sotto soglia, il volume viene azzerato del tutto
        IsAboveGate = volume >= noiseGateThreshold;
        if (!IsAboveGate)
            volume = 0f;

        currentVolume = Mathf.Lerp(currentVolume, volume, Time.deltaTime * smoothing);

        if (volumeBar != null)
            volumeBar.value = Mathf.Clamp01(currentVolume);
    }

    /// <summary>
    /// Da chiamare dal sistema di opzioni/settings quando l'utente cambia la soglia del noise gate.
    /// Esempio: micVolumeMeter.SetNoiseGateThreshold(slider.value);
    /// </summary>
    public void SetNoiseGateThreshold(float value)
    {
        noiseGateThreshold = Mathf.Clamp01(value);
    }

    float GetLoudness()
    {
        int micPosition = Microphone.GetPosition(microphoneDevice) - sampleWindow;
        if (micPosition < 0) return 0f;

        float[] samples = new float[sampleWindow];
        micClip.GetData(samples, micPosition);

        float sum = 0f;
        for (int i = 0; i < sampleWindow; i++)
            sum += samples[i] * samples[i];

        float rms = Mathf.Sqrt(sum / sampleWindow);
        return rms * sensitivity;
    }

    void OnDisable()
    {
        if (!string.IsNullOrEmpty(microphoneDevice) && Microphone.IsRecording(microphoneDevice))
            Microphone.End(microphoneDevice);
    }
}
