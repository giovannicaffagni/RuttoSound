using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float durata = 60f;
    private float tempoCorrente;
    private bool attivo = true;

    void Start()
    {
        tempoCorrente = durata;
    }

    void Update()
    {
        if (!attivo) return;

        tempoCorrente -= Time.deltaTime;
        if (tempoCorrente <= 0f)
        {
            tempoCorrente = 0f;
            attivo = false;
        }

        AggiornaTesto();
    }

    void AggiornaTesto()
    {
        int secondi = Mathf.CeilToInt(tempoCorrente);
        timerText.text = secondi.ToString();
    }

    public void Avvia() { attivo = true; }
    public void Ferma() { attivo = false; }
    public void Reset() { tempoCorrente = durata; attivo = false; AggiornaTesto(); }
}