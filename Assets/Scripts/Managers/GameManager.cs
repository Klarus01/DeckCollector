using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    [SerializeField] private GameObject completionScreen;
    [SerializeField] private List<UpgradeButtonUI> upgradeButtons;

    private const float MinShopItemCost = 2;
    private float shopItemCost = MinShopItemCost;
    private int goldCount = 100;
    private int partCount = 100;

    public Deck deck;
    public CardManager cardManager;
    public CameraManager cameraManager;
    public BuildingManager buildingManager;
    public WaveManager waveManager;
    public UIWaveManager uiWaveManager;
    public ScoreManager scoreManager;
    public ShopManager shopManager;
    
    public event Action OnUIUpdate;
    public event Action OnHandUpdate;

    public float ShopItemCost { get => shopItemCost; set { shopItemCost = Mathf.Max(value, 2); UpdateUI(); } }
    public int GoldCount { get => goldCount; set { goldCount = value; UpdateUI(); } }
    public int PartCount { get => partCount; set { partCount = value; UpdateUI(); } }

    public void UpdateUI() => OnUIUpdate?.Invoke();

    public void UpdateHand() => OnHandUpdate?.Invoke();

    public void AddPoints(int points) => scoreManager?.AddPoints(points);
    
    protected override void Awake()
    {
        base.Awake();
        InitializeManagers();
    }

    private void InitializeManagers()
    {
        deck?.Initialize();
        cardManager?.Initialize();
        buildingManager?.Initialize();
        waveManager?.Initialize();
        uiWaveManager?.Initialize();
        scoreManager?.Initialize();
    }

    public void ShowCompletionScreen()
    {
        CardManager.Instance.cardUIController.SwitchCardHolderVisibility();
        completionScreen.SetActive(true);
        scoreManager.ShowFinalScore();
    }
    
    public void RestartGame()
    {
        ResetGameState();
        ResetUI();
        ResetManagers();
        ResetUpgrades();
        cameraManager.SetUpCameraToStartingPosition();
    }
    
    private void ResetGameState()
    {
        GoldCount = 0;
        PartCount = 0;
        ShopItemCost = MinShopItemCost;
    }
    
    private void ResetUI()
    {
        UpdateUI();
        cardManager.cardUIController.SwitchCardHolderVisibility();
        completionScreen.SetActive(false);
    }
    
    private void ResetManagers()
    {
        cardManager.ResetHand();
        scoreManager.ResetScore(); 
        waveManager.ResetWaves();
        uiWaveManager.ResetWaveUI();
        shopManager.ResetShop();
        buildingManager.ResetBuildings();
    }

    private void ResetUpgrades()
    {
        UpgradeManager.Instance.UpgradesReset();
        foreach (var button in upgradeButtons)
        {
            button.ResetUpgradeStages();
        }
    }
}
