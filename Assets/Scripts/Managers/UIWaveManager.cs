using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWaveManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveProgress
    {
        public Slider slider;
    }

    [SerializeField] private List<WaveProgress> waveProgressBars;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TMP_Text stageText;
    [SerializeField] private TMP_Text waveText;
    
    private int currentStage = 1;
    private int currentWave;

    public void Initialize()
    {
        ResetWaveUI();
        UpdateWaveUI();
    }

    public void UpdateWaveProgress(float progress)
    {
        if (currentWave < waveProgressBars.Count)
        {
            waveProgressBars[currentWave].slider.value = progress;
        }
        
        if (progress >= 1.0f)
        {
            CompleteWave();
        }
    }

    private void UpdateWaveUI()
    {
        stageText.SetText($"Stage: {currentStage}");
        waveText.SetText($"{currentWave + 1} / {waveProgressBars.Count}");
    }
    
    public void CompleteWave()
    {
        if (currentWave < waveProgressBars.Count)
        {
            waveProgressBars[currentWave].slider.value = 1;
            currentWave++;
        }

        if (currentWave >= waveProgressBars.Count)
        {
            currentStage++;
            ResetWaveUI();
        }
        
        UpdateWaveUI();
    }

    public void ResetWaveUI()
    {
        foreach (var waveProgress in waveProgressBars)
        {
            waveProgress.slider.value = 0;
        }

        currentWave = 0;
        currentStage = 1;
    }
}
