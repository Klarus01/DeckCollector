using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text partsText;
    [SerializeField] private TMP_Text shopCostText;

    private void Start()
    {
        GameManager.Instance.OnUIUpdate += UpdateUI;
        UpdateUI();
    }

    public void UpdateUI()
    {
        goldText.SetText(GameManager.Instance.GoldCount.ToString());
        partsText.SetText(GameManager.Instance.PartCount.ToString());
        shopCostText.SetText($"Unit cost: {GameManager.Instance.ShopCost}");
    }
}
