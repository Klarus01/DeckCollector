using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text goldText;
    public TMP_Text partsText;
    public TMP_Text shopCostText;

    private void Start()
    {
        GameManager.Instance.OnUIUpdate += UpdateUI;
        UpdateUI();
    }

    public void UpdateUI()
    {
        goldText.SetText(GameManager.Instance.GoldCount.ToString());
        partsText.SetText(GameManager.Instance.partsCount.ToString());
        shopCostText.SetText($"Unit cost: {GameManager.Instance.ShopCost}");
    }
}
