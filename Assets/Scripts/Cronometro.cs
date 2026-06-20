using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI cronometroText;
    public TextMeshProUGUI tempoMassimoText;
    public MicVolumeMeter micVolumeMeter;
    public float ritardoStop = 0.5f;

    private float tempoCorrente = 0f;
    private float tempoMassimo = 0f;
    private bool attivo = false;
    private float silenzioTimer = 0f;

    void Update()
    {
        if (micVolumeMeter.IsAboveGate)
        {
            attivo = true;
            silenzioTimer = 0f;
        }
        else if (attivo)
        {
            silenzioTimer += Time.deltaTime;
            if (silenzioTimer > ritardoStop)
            {
                attivo = false;
                FermaERegistraMassimo();
                return;
            }
        }

        if (!attivo) return;

        tempoCorrente += Time.deltaTime;

        if (micVolumeMeter.IsAboveGate)
            AggiornaTesto();
    }

    void FermaERegistraMassimo()
    {
        if (tempoCorrente > tempoMassimo)
        {
            tempoMassimo = tempoCorrente;
            if (tempoMassimoText != null)
                tempoMassimoText.text = FormattaTempo(tempoMassimo);
        }

        tempoCorrente = 0f;
        AggiornaTesto();
    }

    void AggiornaTesto()
    {
        cronometroText.text = FormattaTempo(tempoCorrente);
    }

    string FormattaTempo(float tempo)
    {
        int minuti = Mathf.FloorToInt(tempo / 60f);
        int secondi = Mathf.FloorToInt(tempo % 60f);
        int centesimi = Mathf.FloorToInt((tempo * 100f) % 100f);
        return string.Format("{0:00}:{1:00}:{2:00}", minuti, secondi, centesimi);
    }

    public void Avvia() { attivo = true; }
    public void Ferma() { attivo = false; FermaERegistraMassimo(); }
    public void Reset() { tempoCorrente = 0f; tempoMassimo = 0f; attivo = false; AggiornaTesto(); if (tempoMassimoText != null) tempoMassimoText.text = FormattaTempo(0f); }
}
