using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text partsText;
    [SerializeField] private TMP_Text shopCostText;
    [SerializeField] private Button restartGameButton;

    private void Start()
    {
        GameManager.Instance.OnUIUpdate += UpdateUI;
        UpdateUI();
        restartGameButton.onClick.AddListener(OnResetButtonPressed);
    }

    private void OnResetButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }

    private void UpdateUI()
    {
        goldText.SetText(GameManager.Instance.GoldCount.ToString());
        partsText.SetText(GameManager.Instance.PartCount.ToString());
        shopCostText.SetText($"Unit cost: {(int)GameManager.Instance.ShopItemCost}");
    }
}
