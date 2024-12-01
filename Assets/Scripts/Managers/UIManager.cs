using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text partsText;
    [SerializeField] private TMP_Text shopCostText;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        GameManager.Instance.OnUIUpdate += UpdateUI;
        UpdateUI();
        restartGameButton.onClick.AddListener(ResetButtonPressed);
        menuButton.onClick.AddListener(MenuButtonPressed);
    }

    private void ResetButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }

    private void MenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void UpdateUI()
    {
        goldText.SetText(GameManager.Instance.GoldCount.ToString());
        partsText.SetText(GameManager.Instance.PartCount.ToString());
        shopCostText.SetText($"Unit cost: {(int)GameManager.Instance.ShopItemCost}");
    }
}
