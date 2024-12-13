using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gameTimeText;

    private readonly int endScoreMultiplication = 10;
    private int points;
    private float gameTime;

    public void Initialize()
    {
        StartScoreReduction();
        LocalizationSettings.SelectedLocaleChanged += OnLanguageChanged;
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(UnityEngine.Localization.Locale obj)
    {
        ShowFinalScore();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }

    private void StartScoreReduction()
    {
        StartCoroutine(ReduceScoreOverTime());
    }

    private IEnumerator ReduceScoreOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            points = Mathf.Max(0, points - 1);
        }
    }

    private int CalculateFinalScore()
    {
        return points + (GameManager.Instance.GoldCount + GameManager.Instance.PartCount) * endScoreMultiplication;
    }

    public void ShowFinalScore()
    {
        points = CalculateFinalScore();
        var timePlayed = TimeSpan.FromSeconds(gameTime).ToString(@"hh\:mm\:ss");
        gameTimeText.SetText(timePlayed);
        var localizedCost = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("CompletionTable", "Points", new object[] { points });
        localizedCost.Completed += handle =>
        {
            scoreText.SetText(handle.Result);
        };
    }

    public void ResetScore()
    {
        points = 0;
        gameTime = 0f;
    }
}