using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private readonly int endScoreMultiplication = 10;
    private int points;
    public int Points { get => points; set => points = value; } 

    public void Initialize()
    {
        StartScoreReduction();
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
        scoreText.SetText($"Score: {points}");
    }
}