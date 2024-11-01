using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private GameObject completionScreen;
    private int shopItemCost = 2;
    private int goldCount = 5;
    private int partCount;
    public Deck deck;
    public CardManager cardManager;
    public CameraManager cameraManager;
    public BuildingManager buildingManager;
    public WaveManager waveManager;
    public ScoreManager scoreManager;
    public event Action OnUIUpdate;
    public event Action OnHandUpdate;
    
    public int ShopCost { get => shopItemCost; set { shopItemCost = value; OnUIUpdate?.Invoke(); } }
    public int GoldCount { get => goldCount; set { goldCount = value; OnUIUpdate?.Invoke(); } }
    public int PartCount { get => partCount; set { partCount = value; OnUIUpdate?.Invoke(); } }

    private void Start()
    {
        scoreManager.Initialize();
    }

    public void UpdateUI()
    {
        OnUIUpdate?.Invoke();
    }

    public void UpdateHand()
    {
        OnHandUpdate?.Invoke();
    }

    public void AddPoints(int points)
    {
        scoreManager.AddPoints(points);
    }

    public void ShowCompletionScreen()
    {
        completionScreen.SetActive(true);
        scoreManager.ShowFinalScore();
    }
    
    public void RestartGame()
    {
        scoreManager.Points = 0;
        GoldCount = 0;
        PartCount = 0;

        UpdateUI();
        
        cardManager.ResetHand();

        waveManager.ResetWaves();

        buildingManager.ResetBuildings();

        completionScreen.SetActive(false);

        cameraManager.SetUpCameraToStartingPosition();
    }

}
