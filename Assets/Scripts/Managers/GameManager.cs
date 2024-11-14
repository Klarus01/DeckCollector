using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private GameObject completionScreen;
    [SerializeField] private List<UpgradeButtonUI> upgradeButtons; 
    
    private float shopItemCost = 2;
    private int goldCount;
    private int partCount;
    
    public Deck deck;
    public CameraManager cameraManager;
    public BuildingManager buildingManager;
    public WaveManager waveManager;
    public ScoreManager scoreManager;
    public ShopManager shopManager;
    public event Action OnUIUpdate;
    public event Action OnHandUpdate;
    
    public float ShopCost { get => shopItemCost; set { shopItemCost = Mathf.Max(value, 2); OnUIUpdate?.Invoke(); } }
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
        CardManager.Instance.cardUIController.SwitchCardHolderVisibility();
        completionScreen.SetActive(true);
        scoreManager.ShowFinalScore();
    }
    
    public void RestartGame()
    {
        GoldCount = 0;
        PartCount = 0;
        shopItemCost = 2;

        UpdateUI();
        CardManager.Instance.cardUIController.SwitchCardHolderVisibility();
        CardManager.Instance.ResetHand();
        scoreManager.ResetScore();
        waveManager.ResetWaves();
        shopManager.ResetShop();
        UpgradeManager.Instance.UpgradesReset();
        foreach (var button in upgradeButtons)
        {
            button.ResetUpgradeStages();
        }
        buildingManager.ResetBuildings();
        completionScreen.SetActive(false);
        cameraManager.SetUpCameraToStartingPosition();
    }
}
